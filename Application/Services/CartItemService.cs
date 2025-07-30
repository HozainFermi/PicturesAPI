using Application.Models.Pages;
using Application.ServiceInterfaces;
using Domain.RepositoryInterfaces;
using Pictures.Application.DTOs.Carts;
using Pictures.Application.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartItemService : ICartItemService
    {
        public Task<CreateCartItemDto> Create(CreateCartItemDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCartItemDto> DeleteById(Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseCartItemDto> DisableById(Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<PageDto<CartItemPreviewDto>> GetItems(PageParams pageParams, Guid cartId)
        {
            throw new NotImplementedException();
        }

        public Task<bool?> Update(UpdateCartItemDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
