using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Rentals
{
    public class RentalAllInputsDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
