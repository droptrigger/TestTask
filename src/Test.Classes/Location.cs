namespace Test.Classes
{
    /// <summary>
    /// Класс локации
    /// </summary>
    public record class Location
    {
        /// <summary>
        /// Центр
        /// </summary>
        public Point Center { get; set; } = null!;

        /// <summary>
        /// Полигон
        /// </summary>
        public List<Point> Polygon { get; set; } = null!;
    }
}
