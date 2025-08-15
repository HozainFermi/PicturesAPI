using Domain.Entities;
using Domain.Models.Pagination;
using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ICartItemRepository
    {
        public Task<CartEntity> AddCartItem(CartItemEntity entity, CancellationToken cancellationToken);
        public Task<CartItemEntity[]> GetItemsAsync(Guid cartId,PageParams filter,CancellationToken cancellationToken);
        public Task<CartEntity> UpdateItemAsync(Guid itemId ,int newQuantity, CancellationToken cancellationToken);

        public Task<CartEntity> DisableItemAsync(Guid itemId, CancellationToken cancellationToken);
        public Task<CartEntity> DisableAllItemsAsync(Guid cartItem,CancellationToken cancellationToken);
        public Task<CartEntity> AbleAllItemsAsync(Guid cartId, CancellationToken cancellationToken);

        public Task<CartEntity> DeleteItemAsync(Guid id, CancellationToken cancellationToken);
    }
}
