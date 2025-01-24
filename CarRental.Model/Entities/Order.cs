using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Model.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string? Details { get; set; }
        public decimal TotalCost { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
