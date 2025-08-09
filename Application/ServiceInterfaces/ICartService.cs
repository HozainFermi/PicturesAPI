using Domain.Entities;
using Application.DTOs.Carts;
using Domain.Models;

namespace Application.ServiceInterfaces
{
    public interface ICartService
    {
        Task<Guid> Create(UserEntity user);
        Task<CartDto> DeleteCart(Guid id);

        Task<bool> EmptyCart(Guid id);

        Task<PageDto<CartItemPreviewDto>> GetCartByUserId(PageParams pageParams, Guid userId);

    }
}
