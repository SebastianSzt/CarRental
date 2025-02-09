﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Rentals
{
    public class RentalInputDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
