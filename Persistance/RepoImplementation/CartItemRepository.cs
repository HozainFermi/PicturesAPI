using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.Pagination;
using Domain.Models.Products;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistance.Data;
using Persistance.Extensions;
using System.Threading;

namespace Persistance.RepoImplementation
{
    public class CartItemRepository:ICartItemRepository
    {
        private readonly ILogger<CartItemRepository> _logger;
        private readonly PicturesDbContext _DbContext;

        public CartItemRepository(ILogger<CartItemRepository> logger, PicturesDbContext picturesDb) 
        {
            _logger = logger;
            _DbContext = picturesDb;

        }

        public async Task<CartEntity> AddCartItem(CartItemEntity entity, CancellationToken cancellationToken)
        {
            using (var transaction = await _DbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var addedItem = await _DbContext.CartItems.AddAsync(entity, cancellationToken);
                    var cart = await _DbContext.Carts.Include(c=>c.CartItems).Where(c=>c.Id==entity.CartId).FirstAsync(cancellationToken);

                    if (cart == null) { throw new EntityNotFoundException(entity.CartId, typeof(CartEntity)); }
                    cart.Price += entity.UnitPrice;
                    await _DbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return cart;

                }
                catch (Exception ex)
                {
                    _logger.LogError("Ошибка транзации во время добавления cartItem", ex);
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<CartEntity> DeleteItemAsync(Guid id, CancellationToken cancellationToken)
        {
            using (var transaction = await _DbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var foundItem = await _DbContext.CartItems.FindAsync(id, cancellationToken);
                    if (foundItem == null) { throw new EntityNotFoundException(id, typeof(CartItemEntity)); }
                    _DbContext.CartItems.Remove(foundItem);
                    await _DbContext.SaveChangesAsync(cancellationToken);

                    var foundCart = await _DbContext.Carts
                            .Include(c => c.CartItems)
                            .FirstAsync(c => c.Id == foundItem.CartId, cancellationToken);
                    if (foundCart == null) { throw new EntityNotFoundException(foundItem.CartId, typeof(CartEntity)); }

                    foundCart.Price -= foundItem.UnitPrice;
                    await _DbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return foundCart;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ошибка транзации во время добавления cartItem", ex);
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<CartEntity> DisableItemAsync(Guid id, CancellationToken cancellationToken)
        {
            using (var transaction = await _DbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var foundItem = await _DbContext.CartItems.FindAsync(id, cancellationToken);
                    if (foundItem == null) { throw new EntityNotFoundException(id, typeof(CartItemEntity)); }

                    foundItem.IsActive = false;
                    await _DbContext.SaveChangesAsync(cancellationToken);

                    var foundCart = await _DbContext.Carts.Include(c=>c.CartItems).Where(c=>c.Id==foundItem.CartId).FirstOrDefaultAsync(cancellationToken);
                    if (foundCart == null) { throw new EntityNotFoundException(foundItem.CartId, typeof(CartEntity)); }

                    foundCart.Price -= foundItem.UnitPrice;
                    await _DbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return foundCart;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ошибка транзации во время добавления cartItem", ex);
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }


        public async Task<CartEntity> AbleAllItemsAsync(Guid cartId, CancellationToken cancellationToken)
        {
            var cart = await _DbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
            if (cart == null) { throw new EntityNotFoundException(cartId, typeof(CartEntity)); }
            cart.CartItems.Select(i=>i.IsActive=true);
            await _DbContext.SaveChangesAsync(cancellationToken);
            return cart;

        }
               
        public async Task<CartEntity> DisableAllItemsAsync(Guid cartId, CancellationToken cancellationToken)
        {
            var cart = await _DbContext.Carts.FindAsync(cartId, cancellationToken);
            if (cart == null) { throw new EntityNotFoundException(cartId, typeof(CartEntity)); }

            cart.CartItems.Select(i => i.IsActive = false);
            await _DbContext.SaveChangesAsync(cancellationToken);
            return cart;
        }
  
        public async Task<CartItemEntity[]> GetItemsAsync(Guid cartId,PageParams pageParams, CancellationToken cancellationToken)
        {

           return await _DbContext.CartItems.Page(cartId,pageParams);
        }

        public async Task<CartEntity> UpdateItemAsync(Guid itemId, int newQuantity, CancellationToken cancellationToken)
        {
            using (var transaction = await _DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var item =await _DbContext.CartItems.Include(i=>i.Cart).FirstOrDefaultAsync(i=>i.Id==itemId,cancellationToken);
                    if (item == null) { throw new EntityNotFoundException(itemId, typeof(CartItemEntity)); }
                    var priceChange = (newQuantity - item.Quantity) * item.UnitPrice;
                    
                    item.Quantity = newQuantity;
                    item.Cart.Price += priceChange; 

                    await _DbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return item.Cart; // Возвращаем обновлённую корзину
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex,"Ошибка при обновлении CartItem {cartItemId}",itemId);
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
