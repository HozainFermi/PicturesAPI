using Application.DTOs.Users;
using Application.ServiceInterfaces;
using Domain.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class UserService : IUserService
    {
        public Task<UserDto> Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PageDto<UserDto>> GetAll(PageParams pageParams)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
