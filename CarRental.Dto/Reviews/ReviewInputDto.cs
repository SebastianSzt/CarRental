using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Reviews
{
    public class ReviewInputDto
    {
        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
