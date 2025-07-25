﻿using Test.Classes;
using Test.Classes.DTOs.Requests;
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

            if (field is null)
                return -1.0;

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
        public async Task<double> GetDistanceToTheCenterAsync(GetDistanceDTO getDistanceDTO)
        {
            var center = await _fieldRepository.GetByIdAsync(getDistanceDTO.Id);

            if (center is null)
                return -1.0;

            Point point = new()
            {
                Latitude = getDistanceDTO.Lat,
                Longitude = getDistanceDTO.Lng
            };

            return DistanceCalculator.Calculate(point, center.Location.Center);
        }

        /// <summary>
        /// Асинхронная проверка принадлежности точки к какому-нибудь из полей
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>Объект класса <see cref="Field"/>, иначе null</returns>
        public async Task<Field?> GetFieldInsideAsync(Point point)
        {
            var fields = await _fieldRepository.GetAllAsync();

            if (fields is null)
                return null;

            return InsideChecker.Check(point, fields);
        }
    }
}
