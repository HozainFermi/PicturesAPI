using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDK.Application.Models.Orders;
using IDK.Application.Models.Users;

namespace IDK.Application.Abstractions
{
    public interface IUserService
    {
       
        Task<UserDto> GetById(Guid userId);        
        Task<List<UserDto>> GetAll();
        Task<UserDto> Delete(Guid userId);


    }
}
