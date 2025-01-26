using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Model.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
