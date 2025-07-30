using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ICartItemRepository
    {
        public Task<CartItemEntity> Get(Guid id);
        public Task<CartItemEntity> Create(ProductEntity product);
        public Task<CartItemEntity> Update(CartItemEntity entity);
        public Task<CartItemEntity> Delete(Guid id);
    }
}
