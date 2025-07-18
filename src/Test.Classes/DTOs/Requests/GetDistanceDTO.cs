using System.ComponentModel.DataAnnotations;

namespace Test.Classes.DTOs.Requests
{
    public class GetDistanceDTO
    {
        /// <summary>
        /// Широта
        /// </summary>
        [Required]
        public double Lat { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [Required]
        public double Lng { get; set; }

        /// <summary>
        /// Id поля
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
