using Test.Classes;
using Test.Core.Extensions;
using Test.Core.Repositories.Interfaces;

namespace Test.Core.Services
{
    /// <summary>
    /// Сервис для работы с полями
    /// </summary>
    public class FieldService
    {
        private readonly IFieldRepository _fieldRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        public FieldService(IFieldRepository fieldRepository) 
        {
            _fieldRepository = fieldRepository;
        }

        /// <summary>
        /// Асинхронное получение всех полей 
        /// </summary>
        /// <returns>
        /// Список <see cref="List{Field}"/> полей
        /// </returns>
        public async Task<List<Field>>? GetAllFieldsAsync() 
        {
            return await _fieldRepository.GetAllAsync();
        }

        /// <summary>
        /// Асинхронное получение площади по Id
        /// </summary>
        /// <param name="id">Идентификатор поля</param>
        /// <returns>
        /// Площадь в метрах кваратных, иначе -1 
        /// </returns>
        public async Task<double> GetSizeWithIdAsync(int id)
        {
            var field = await _fieldRepository.GetByIdAsync(id);

            return field.Size;
        }

        /// <summary>
        /// Асинхронное получение расстояния от точки до центра поля
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="fieldId">Идентификатор поля, у которого взять центр</param>
        /// <returns>
        /// Расстояние в метрах, иначе -1
        /// </returns>
        public async Task<double> GetDistanceToTheCenterAsync(Point point, int fieldId)
        {
            var center = await _fieldRepository.GetByIdAsync(fieldId);

            if (center is null)
                return -1.0;

            return DistanceCalculator.Calculate(point, center.Location.Center);
        }
    }
}
