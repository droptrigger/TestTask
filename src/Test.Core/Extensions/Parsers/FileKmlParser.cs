using System.Xml.Linq;
using Test.Classes;
using Test.Core.Extensions.Parsers.Base;
using Location = Test.Classes.Location;

namespace Test.Core.Extensions.Parsers
{
    /// <summary>
    /// Парсер из .kml файлов, реализация <see cref="IFileParser{T}"/>
    /// </summary>
    public class FileKmlParser : IFileParser<List<Field>>
    {
        public async Task<List<Field>?> Parse(string centroidsPath, string fieldsPath)
        {
            var ns = XNamespace.Get("http://www.opengis.net/kml/2.2");

            using var centroidStream = File.OpenRead(centroidsPath);
            using var fieldStream = File.OpenRead(fieldsPath);

            var centroidDoc = await XDocument.LoadAsync(centroidStream, LoadOptions.None, default);
            var fieldDoc = await XDocument.LoadAsync(fieldStream, LoadOptions.None, default);

            var centerDict = centroidDoc
                .Descendants(ns + "Placemark")
                .Select(placemark => new
                {
                    Fid = ParseFieldId(placemark, ns),
                    Center = PointParser.Parse(placemark.Descendants(ns + "coordinates").FirstOrDefault()?.Value!)
                })
                .Where(placemark => placemark.Fid.HasValue)
                .ToDictionary(placemark => placemark.Fid!.Value, placemark => placemark.Center);

            var result = new List<Field>();

            foreach (var placemark in fieldDoc.Descendants(ns + "Placemark"))
            {
                var fid = ParseFieldId(placemark, ns);
                if (!fid.HasValue) continue;

                var name = placemark.Element(ns + "name")?.Value ?? fid.Value.ToString();
                var coordsRaw = placemark.Descendants(ns + "LinearRing")
                                         .Descendants(ns + "coordinates")
                                         .FirstOrDefault()?.Value;

                if (string.IsNullOrWhiteSpace(coordsRaw))
                    continue;

                var polygon = PolygonParser.Parse(coordsRaw);
                if (polygon.Count < 3)
                    continue;

                centerDict.TryGetValue(fid.Value, out var center);
                center ??= polygon[0];

                double area = FieldSizeCalculator.Calculate(polygon);

                result.Add(new Field
                {
                    Id = fid.Value,
                    Name = name,
                    Size = area,
                    Location = new Location
                    {
                        Center = center,
                        Polygon = polygon
                    }
                });
            }

            return result;
        }

        /// <summary>
        /// Пытается извлечь и распарсить идентификатор из элемента Placemark
        /// </summary>
        /// <param name="placemark">Элемент Placemark, содержащий SimpleData с атрибутом name="fid"</param>
        /// <param name="ns">Пространство имён XML KML-файла.</param>
        /// <returns>Идентификатор, иначе null</returns>
        private static int? ParseFieldId(XElement placemark, XNamespace ns)
        {
            var fidValue = placemark.Descendants(ns + "SimpleData")
                .FirstOrDefault(e => e.Attribute("name")?.Value == "fid")?.Value;

            return int.TryParse(fidValue, out var fid) ? fid : null;
        }
    }
}
