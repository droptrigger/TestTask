using Test.Classes;

namespace Test.Core.Extensions
{
    /// <summary>
    /// Статический класс для расчета расстояния между точками
    /// </summary>
    public static class DistanceCalculator
    {
        /// <summary>
        /// Вычисляет расстояние между двумя точками по формуле Хаверсина
        /// </summary>
        /// <remarks>
        /// <code>
        ///     Δφ = φ2 - φ1
        ///     Δλ = λ2 - λ1
        ///     a = sin²(Δφ / 2) + cos(φ1) * cos(φ2) * sin²(Δλ / 2)
        ///     c = 2 * atan2(√a, √(1 - a))
        ///     d = R * c
        /// </code>
        /// </remarks>
        /// <param name="firstPoint">Первая точка</param>
        /// <param name="secondPoint">Вторая точка</param>
        /// <returns>Расстояние в метрах</returns>
        public static double Calculate(Point firstPoint, Point secondPoint)
        {
            // Радиус Земли в метрах
            const double R = 6371000; 

            double dLat = ToRadians(secondPoint.Latitude - firstPoint.Latitude);
            double dLon = ToRadians(secondPoint.Longitude - firstPoint.Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(firstPoint.Latitude)) * Math.Cos(ToRadians(secondPoint.Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return Math.Round(R * c, 2);
        }

        private static double ToRadians(double angle) => angle * Math.PI / 180.0;
    }
}
