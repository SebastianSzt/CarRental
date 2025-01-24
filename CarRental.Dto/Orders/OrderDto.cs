using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public decimal TotalCost { get; set; }
        public int ProductCount { get; set; }
    }
}
