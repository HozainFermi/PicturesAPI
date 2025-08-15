using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    public class UserDto
    {
        public string? Email { get; set; }       
        public string Username { get; set; } = null!;
        public DateTime BirthDate { get; set; }        
        public string Role { get; set; }
        public string  Password { get; set; }

    }
}
