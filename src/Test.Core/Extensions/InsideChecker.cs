using Test.Classes;

namespace Test.Core.Extensions
{
    /// <summary>
    /// Статический класс для проверки находится ли точка внутри каком-либо из полей или нет
    /// </summary>
    public static class InsideChecker
    {
        /// <summary>
        /// Проверяет принадлежность точки к какому-либо полю
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="fields">Поля</param>
        /// <returns>
        /// Объект класса <see cref="Field"/>, иначе null
        /// </returns>
        public static Field? Check(Point point, List<Field> fields)
        {
            foreach (var field in fields)
            {
                if (IsPointInPolygon(point, field.Location.Polygon))
                    return field;
            }

            return null;
        }

        /// <summary>
        /// Проверка попадания точки в полигон по алгоритму луча
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="polygon">Полигон</param>
        /// <returns>
        /// True - попадает <br/>
        /// False - не попадает
        /// </returns>
        public static bool IsPointInPolygon(Point point, List<Point> polygon)
        {
            int count = polygon.Count;
            bool result = false;
            int j = count - 1;

            for (int i = 0; i < count; i++)
            {
                if ((polygon[i].Latitude > point.Latitude) != (polygon[j].Latitude > point.Latitude) &&
                    (point.Longitude < (polygon[j].Longitude - polygon[i].Longitude) *
                    (point.Latitude - polygon[i].Latitude) /
                    (polygon[j].Latitude - polygon[i].Latitude) + polygon[i].Longitude))
                {
                    result = !result;
                }

                j = i;
            }

            return result;
        }
        
    }
}
