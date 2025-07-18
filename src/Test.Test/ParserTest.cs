using Test.Classes;
using Test.Core.Extensions;
using Test.Core.Extensions.Parsers;
using Xunit.Abstractions;

namespace Test.Test
{
    public class ParserTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _centroidsPath;
        private readonly string _fieldsPath;

        public ParserTest(ITestOutputHelper output)
        {
            _output = output;
            string rootPath = ProjectRootPath.GetProjectRootPath();
            _centroidsPath = Path.Combine(rootPath, "Points", "centroids.kml");
            _fieldsPath = Path.Combine(rootPath, "Points", "fields.kml");
        }

        [Fact]
        public async void TestPrintAllFieldsInfo()
        {
            FileKmlParser parser = new FileKmlParser();

            List<Field> fields = await parser.Parse(_centroidsPath, _fieldsPath);
            
            foreach (var field in fields)
            {
                _output.WriteLine($"Id: {field.Id}");
                _output.WriteLine($"Name: {field.Name}");
                _output.WriteLine($"Size: {field.Size:F2}");

                _output.WriteLine("Locations:");
                _output.WriteLine($"\tCenter: lat = {field.Location.Center.Latitude}, lng = {field.Location.Center.Longitude}");

                _output.WriteLine("\tPolygon:");
                foreach (var pt in field.Location.Polygon)
                {
                    _output.WriteLine($"\t\t- lat = {pt.Latitude}, lng = {pt.Longitude}");
                }

                _output.WriteLine(new string('-', 40));
            }
        }
    }
}
