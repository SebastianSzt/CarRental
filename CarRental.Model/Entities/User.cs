using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Model.Entities
{
    public class User : IdentityUser
    {
        public override string Id { get; set; }
        public override string Email { get; set; }
        public override string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
