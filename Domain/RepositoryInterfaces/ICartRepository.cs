using Domain.Entities;
using Domain.Models.Pagination;

namespace Domain.RepositoryInterfaces
{
    public interface ICartRepository
    {
        public Task<CartEntity> AddCartAsync(CartEntity cart, CancellationToken cancellationToken);

        public Task<CartItemEntity[]> GetCartItemsByUserIdAsync(Guid userId, PageParams pageParams ,CancellationToken cancellationToken);
        public Task<CartEntity> GetCartByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<CartEntity> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        public Task<UserEntity> GetUserByCartIdAsync(Guid cartId, CancellationToken cancellationToken);

        public Task<CartEntity> CleanCartByIdAsync(Guid cartId, CancellationToken cancellationToken);
    }
}
