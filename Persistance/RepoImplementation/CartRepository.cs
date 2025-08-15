using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.RepoException;
using Domain.Models.Pagination;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistance.Data;
using Persistance.Extensions;

namespace Persistance.RepoImplementation
{
    public class CartRepository : ICartRepository
    {
        PicturesDbContext _dbContext;
        ILogger<CartRepository> _logger;

        public CartRepository(PicturesDbContext context, ILogger<CartRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<CartEntity> AddCartAsync(CartEntity cart, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Carts.AddAsync(cart, cancellationToken);
            return result.Entity;
        }

        public async Task<CartEntity> CleanCartByIdAsync(Guid cartId, CancellationToken cancellationToken)
        {
            
            CartEntity? foundCart = await _dbContext.Carts
                .Include(ci => ci.CartItems)
                .FirstOrDefaultAsync(ci => ci.Id == cartId, cancellationToken);
           
            if (foundCart == null)
            {
                throw new EntityNotFoundException(cartId, typeof(CartEntity));
            }            
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {                 
                    _dbContext.CartItems.RemoveRange(foundCart.CartItems);                   
                    foundCart.CartItems.Clear();     
                    foundCart.Price = 0;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return foundCart;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при удалении элементов корзины {cartid}", cartId);
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

        public async Task<CartEntity> GetCartByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            CartEntity? foundCart = await _dbContext.Carts.FindAsync(id, cancellationToken);
            if (foundCart == null) { _logger.LogError("Корзина не найдена по Id {CartId}", id); throw new EntityNotFoundException(id, typeof(CartEntity)); }

            return foundCart;

        }

        public async Task<CartEntity> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
           CartEntity? foundCart = await _dbContext.Carts.Where(c=>c.OwnerId==userId).FirstOrDefaultAsync(cancellationToken);
            if (foundCart == null) { _logger.LogError("Корзина не найдена по Id пользователя {UserId}", userId); throw new EntityNotFoundException(userId, typeof(CartEntity)); }
            return foundCart;
        }

        public async Task<CartItemEntity[]> GetCartItemsByUserIdAsync(Guid userId, PageParams pageParams, CancellationToken cancellationToken)
        {
            var foundCart = _dbContext.Carts.Where(c => c.OwnerId == userId);

            return await foundCart.Page(pageParams,cancellationToken);

        }

        
        public async Task<CartEntity> DeleteCartByIdAsync(Guid cartId, CancellationToken cancellationToken)
        {
            
            var foundCart = await _dbContext.Carts
                .Include(c => c.CartItems) 
                .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);

            if (foundCart == null)
            {
                throw new EntityNotFoundException(cartId, typeof(CartEntity));
            }

            
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
               
                if (foundCart.CartItems.Any())
                {
                    _dbContext.CartItems.RemoveRange(foundCart.CartItems);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                
                _dbContext.Carts.Remove(foundCart);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return foundCart;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Concurrency error while deleting cart {CartId}", cartId);
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Database error while deleting cart {CartId}", cartId);
                throw new RepositoryException("Failed to delete cart due to database error", ex);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Unexpected error while deleting cart {CartId}", cartId);
                throw new RepositoryException("Failed to delete cart", ex);
            }
        }
    }
}
