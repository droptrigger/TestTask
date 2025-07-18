namespace Test.Classes
{
    /// <summary>
    /// Класс поля
    /// </summary>
    public record class Field
    {
        /// <summary>
        /// Идентификатор поля
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название поля
        /// </summary>
        public string Name { get; set; } = null!;
        
        /// <summary>
        /// Размер поля
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// Локация поля
        /// </summary>
        public Location Location { get; set; } = null!;
    }
}
