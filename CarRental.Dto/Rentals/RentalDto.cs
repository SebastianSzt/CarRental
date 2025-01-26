using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Rentals
{
    public class RentalDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
