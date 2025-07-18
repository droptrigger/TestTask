using System.Globalization;
using Test.Classes;

namespace Test.Core.Extensions.Parsers
{
    /// <summary>
    /// Статический класс для парсинга полигона из строки
    /// </summary>
    public static class PolygonParser
    {
        /// <summary>
        /// Парсинг в список точек (полигон)
        /// </summary>
        /// <param name="raw">Строка с координатами</param>
        /// <returns>Список точек <see cref="List{Point}"/></returns>
        public static List<Point> Parse(string raw)
        {
            var points = raw.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return points
                .Select(point => PointParser.Parse(point))
                .ToList();
        }
    }
}
