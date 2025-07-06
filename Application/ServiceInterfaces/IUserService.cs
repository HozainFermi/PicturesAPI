using Pictures.Application.DTOs.Users;

namespace IDK.Application.Abstractions
{
    public interface IUserService
    {
       
        Task<UserDto> GetById(Guid userId);        
        Task<List<UserDto>> GetAll();
        Task<UserDto> Delete(Guid userId);


    }
}
