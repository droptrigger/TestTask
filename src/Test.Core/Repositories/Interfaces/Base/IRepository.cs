namespace Test.Core.Repositories.Interfaces.Base
{
    /// <summary>
    /// Базовый интерфейс для репозиториев
    /// </summary>
    /// <typeparam name="T">Возвращаемый класс</typeparam>
    public interface IRepository<T> 
        where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
    }
}
