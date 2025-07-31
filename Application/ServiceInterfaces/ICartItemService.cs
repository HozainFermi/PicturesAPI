using Application.DTOs.Carts;
using Application.DTOs.Pagination;


namespace Application.ServiceInterfaces
{
    public interface ICartItemService
    {
        public Task<CreateCartItemDto> Create(CreateCartItemDto dto);
        public Task<PageDto<CartItemPreviewDto>> GetItems(PageParams pageParams, Guid cartId);
        public Task<ResponseCartItemDto> DeleteById(Guid itemId);
        public Task<ResponseCartItemDto> DisableById(Guid itemId);
        public Task<bool?> Update(UpdateCartItemDto updateDto);

    }
}
