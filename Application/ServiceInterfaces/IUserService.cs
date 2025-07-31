using Application.DTOs.Pagination;
using Application.DTOs.Users;

namespace Application.ServiceInterfaces
{
    public interface IUserService
    {
       
        Task<UserDto> GetById(Guid userId);        
        Task<PageDto<UserDto>> GetAll();
        Task<UserDto> Delete(Guid userId);


    }
}
