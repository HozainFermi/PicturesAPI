using Application.Models.Pages;
using Domain.Entities;
using Pictures.Application.DTOs.Carts;
using Pictures.Application.DTOs.Orders;
using Pictures.Application.DTOs.Users;
using Pictures.Application.Models.Pages;

namespace IDK.Application.Abstractions
{
    public interface IUserService
    {
       
        public Task<UserDto> GetById(Guid userId);
        public Task<PageDto<OrderDto>> GetUserOrders(PageParams pageParams,Guid id);
        public Task<PageDto<CartItemDto>> GetUserCartItems(PageParams pageParams, Guid id);
        public Task<PageDto<UserDto>> GetAll(PageParams pageParams );
        public Task<UserDto> Delete(Guid userId);


    }
}
