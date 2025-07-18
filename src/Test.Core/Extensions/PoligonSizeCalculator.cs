using Test.Classes;

namespace Test.Core.Extensions
{
    /// <summary>
    /// Статический класс для расчета площади полигона
    /// </summary>
    public static class FieldSizeCalculator
    {
        /// <summary>
        /// Вычисляет площадь полигона по формуле Гаусса (шнуровки)
        /// </summary>
        /// <param name="polygon">Список точек полигона</param>
        /// <returns>Площадь в квадратных метрах</returns>
        public static double Calculate(List<Point> polygon)
        {
            if (polygon == null || polygon.Count < 3)
                return 0;

            if (!IsClosed(polygon))
                return 0;

            // Приближённое количество метров в одном градусе широты
            const double metersPerDegreeLat = 111320.0;

            double avgLat = 0;
            polygon.ForEach(p => avgLat += p.Latitude);
            avgLat /= polygon.Count;

            double metersPerDegreeLng = metersPerDegreeLat * Math.Cos(avgLat * Math.PI / 180.0);
            var baseLat = polygon[0].Latitude;
            var baseLng = polygon[0].Longitude;

            var xy = new List<(double x, double y)>();
            foreach (var p in polygon)
            {
                xy.Add((
                    (p.Longitude - baseLng) * metersPerDegreeLng,
                    (p.Latitude - baseLat) * metersPerDegreeLat
                ));
            }

            double area = 0;
            for (int i = 0; i < xy.Count; i++)
            {
                var (x1, y1) = xy[i];
                var (x2, y2) = xy[(i + 1) % xy.Count];
                area += x1 * y2 - x2 * y1;
            }

            return Math.Round(Math.Abs(area) / 2.0, 2);
        }

        /// <summary>
        /// Проверка полигона на закрытость
        /// </summary>
        /// <param name="polygon">Список точек</param>
        /// <returns>
        /// True - полигон закрыт <br/>
        /// False - полигон не закрыт
        /// </returns>
        private static bool IsClosed(List<Point> polygon)
        {
            var first = polygon[0];
            var last = polygon[^1];
            return first.Latitude == last.Latitude 
                && first.Longitude == last.Longitude;
        }
    }
}
