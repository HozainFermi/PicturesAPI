using Domain.Entities;
using Pictures.Application.DTOs.Carts;
using Pictures.Application.Models.Pages;
using Pictures.Application.Models.ProductExtensions;

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
