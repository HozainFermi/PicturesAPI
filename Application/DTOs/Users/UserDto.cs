using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Application.DTOs.Users
{
    public class UserDto
    {
        public string? Email { get; set; }       
        public string UserName { get; set; } 
        public DateTime BirthDate { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

    }
}
