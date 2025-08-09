using Domain.Models;
using Application.DTOs.Users;

namespace Application.ServiceInterfaces
{
    public interface IUserService
    {
       
        Task<UserDto> GetById(Guid userId);        
        Task<PageDto<UserDto>> GetAll(PageParams pageParams);
        Task<UserDto> Delete(Guid userId);


    }
}
