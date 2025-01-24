using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Categories
{
    public class CategoryInputDto
    {
        [Required]
        [MaxLength(2000)]
        public string Name { get; set; }
    }
}
