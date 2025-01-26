using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
