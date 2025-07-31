using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.RepoImplementation
{
    public class CartRepository : ICartRepository
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

        public Task<ICollection<CartItemEntity>> GetCartItemsByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserByCartId(Guid cartId)
        {
            throw new NotImplementedException();
        }
    }
}
