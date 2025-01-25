using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Model.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public int Year { get; set; }
        public string? FuelType { get; set; }
        public decimal FuelConsumption { get; set; }
        public decimal PricePerDay { get; set; }
        public string? Location { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
