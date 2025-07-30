using Application.Models.Pages;
using Domain.Entities;
using Pictures.Application.DTOs.Carts;
using Pictures.Application.Models.Pages;

namespace Application.ServiceInterfaces
{
    public interface ICartService
    {
        public Task<PageDto<CartItemEntity>> GetCartItemsByUserId(PageParams pageParams,Guid userId);
        public Task<CartEntity> GetCartById(Guid id);
        public Task<CartEntity> GetCartByUserId(Guid userId);
        public Task<CartEntity> CleanCartById(Guid cartId);

        

    }
}
