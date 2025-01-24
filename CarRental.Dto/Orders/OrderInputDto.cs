using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Orders
{
    public class OrderInputDto
    {
        [Required]
        [MaxLength(2000)]
        public string Details { get; set; }

        [Required]
        public decimal TotalCost { get; set; }
    }
}
