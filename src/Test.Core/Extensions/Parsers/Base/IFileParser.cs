namespace Test.Core.Extensions.Parsers.Base
{
    /// <summary>
    /// Интерфейс для реализации парсеров из файлов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFileParser<T>
        where T : class
    {
        /// <summary>
        /// Метод парсинга
        /// </summary>
        /// <returns></returns>
        public Task<T> Parse(string centroidsPath, string fileldsPath);
    }
}
