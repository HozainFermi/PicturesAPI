using Application.DTOs.Users;
using Domain.Models.Pagination;

namespace Application.ServiceInterfaces
{
    public interface IUserService
    {
       
        Task<UserDto> GetById(Guid userId);        
        Task<PageDto<UserDto>> GetAll(PageParams pageParams);
        Task<UserDto> Delete(Guid userId);


    }
}
