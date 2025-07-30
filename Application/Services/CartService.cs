using Application.Models.Pages;
using Application.ServiceInterfaces;
using Domain.Entities;
using Pictures.Application.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        public Task<CartEntity> CleanCartById(Guid cartId)
        {
            throw new NotImplementedException();
        }

        public Task<CartEntity> GetCartById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CartEntity> GetCartByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PageDto<CartItemEntity>> GetCartItemsByUserId(PageParams pageParams, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
