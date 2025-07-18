namespace Test.Classes
{
    /// <summary>
    /// Класс точки на карте
    /// </summary>
    public record class Point
    {
        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }
    }
}
