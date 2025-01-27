using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Cars
{
    public class CarInputDto
    {
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        [Range(1900, 2100)]
        //[MaxLength(4)]
        public int Year { get; set; }

        [Required]
        [MaxLength(50)]
        public string FuelType { get; set; }

        [Required]
        public decimal FuelConsumption { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
