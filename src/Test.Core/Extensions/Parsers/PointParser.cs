using System.Globalization;
using Test.Classes;

namespace Test.Core.Extensions.Parsers
{
    /// <summary>
    /// Статический класс для парсинга точки из строки
    /// </summary>
    public static class PointParser
    {
        /// <summary>
        /// Парсинг в класс точки
        /// </summary>
        /// <param name="raw">Строка</param>
        /// <returns>Точка <see cref="Point"/></returns>
        public static Point Parse(string raw)
        {
            var parts = raw.Trim().Split(',');
            return new Point
            {
                Latitude = double.Parse(parts[0], CultureInfo.InvariantCulture),
                Longitude = double.Parse(parts[1], CultureInfo.InvariantCulture)
            };
        }
    }
}
