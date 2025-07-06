using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ICartRepository
    {
        public Task<ICollection<CartItemEntity>> GetCartItemsByUserId(Guid userId);
        public Task<CartEntity> GetCartById(Guid id);
        public Task<CartEntity> GetCartByUserId(Guid userId);
        public Task<UserEntity> GetUserByCartId(Guid cartId);

        public Task<CartEntity> CleanCartById(Guid cartId);
    }
}
