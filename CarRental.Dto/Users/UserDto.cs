using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Dto.Users
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RentalCount { get; set; }
        public int ReviewCount { get; set; }
        public string UserName { get; set; }
    }
}
