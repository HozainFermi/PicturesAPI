using Domain.Entities;
using IDK.Application.Models.Pages;
using IDK.Application.ProductExtensions;
using Pictures.Application.DTOs.Carts;

namespace Application.ServiceInterfaces
{
    public interface ICartService
    {
        Task<Guid> Create(UserEntity user);
        Task<CartDto> DeleteCart(Guid id);

        Task<CartDto> EmptyCart(Guid id);

        Task<PageDto<CartItemPreviewDto>> GetCartByUserId(PageParams pageParams, Guid userId);

    }
}
