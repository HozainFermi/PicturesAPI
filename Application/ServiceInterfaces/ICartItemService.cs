using Application.DTOs.Carts;
using IDK.Application.Models.Pages;
using IDK.Application.ProductExtensions;
using Pictures.Application.DTOs.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface ICartItemService
    {
        public Task<CreateCartItemDto> Create(CartItemDto dto);
        public Task<PageDto<CartItemPreviewDto>> GetItems(PageParams pageParams, Guid cartId);
        public Task<CartItemDto> DeleteById(Guid itemId);
        public Task<CartItemDto> DisableById(Guid itemId);
        public Task<bool?> Update(UpdateCartItemDto updateDto);

    }
}
