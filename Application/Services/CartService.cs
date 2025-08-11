using Application.DTOs.Carts;
using Application.ServiceInterfaces;
using Domain.Entities;
using Domain.Models.Pagination;


namespace Application.Services
{
    public class CartService : ICartService
    {
        public Task<Guid> Create(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> DeleteCart(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmptyCart(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PageDto<CartItemPreviewDto>> GetCartByUserId(PageParams pageParams, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
