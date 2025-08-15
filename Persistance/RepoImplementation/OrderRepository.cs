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
    public class OrderRepository : IOrderRepository
    {
        PicturesDbContext _dbContext;
        ILogger<OrderRepository> _logger;

        public OrderRepository(PicturesDbContext dbContext, ILogger<OrderRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<OrderEntity> AddOrderAsync(OrderEntity order, CancellationToken cancellationToken)
        {
            
            var added = await _dbContext.Orders.AddAsync(order,cancellationToken);
            return added.Entity;

        }


        public async Task<OrderEntity> DeleteAsync(Guid orderId, CancellationToken cancellationToken)
        {
            OrderEntity? existingOrder = await _dbContext.Orders.FindAsync(orderId, cancellationToken);
            if (existingOrder == null) { throw new EntityNotFoundException(orderId, typeof(OrderEntity)); }
            
            _dbContext.Entry(existingOrder).State = EntityState.Deleted;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return existingOrder;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при удалении заказа {OrderId}", existingOrder.Id);
                throw new RepositoryException("Database error while deleting order", ex);
            }

            
        }

        public async Task<OrderEntity> UpdateOrderAsync(OrderEntity updateOrder, CancellationToken cancellationToken)
        {
            OrderEntity? existingOrder = await _dbContext.Orders.FindAsync(updateOrder.Id, cancellationToken);
            if (existingOrder == null) { throw new EntityNotFoundException(updateOrder.Id, typeof(OrderEntity)); }

            _dbContext.Entry(existingOrder).CurrentValues.SetValues(updateOrder);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return existingOrder;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении заказа {OrderId}", existingOrder.Id);
                throw new RepositoryException("Database error while updating order", ex);
            }
        }

        public Task<OrderEntity> UpdateOrderDeliveryByIdAsync(Guid id, string newDeliveryAddress, CancellationToken cancellationToken)
        {
            //OrderEntity? existingOrder = await _dbContext.Orders.FindAsync(updateOrder.Id, cancellationToken);
            //if (existingOrder == null) { throw new EntityNotFoundException(updateOrder.Id, typeof(OrderEntity)); }

            //_dbContext.Entry(existingOrder).Entity.
            throw new NotImplementedException();
        }

       

        public async Task<OrderEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            OrderEntity? found = await _dbContext.Orders.FindAsync(id);
            if (found == null) { throw new EntityNotFoundException(id, typeof(OrderEntity)); }
            return found;
            
                
            
        }

        public async Task<OrderEntity[]> GetByUserIdAsync(Guid userId, PageParams pageParams, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Orders.Where(o => o.BuyerId == userId).Page(pageParams);
            }
            catch (Exception ex) { throw new RepositoryException("Faild to get orders by user id", ex); }
        }
    }
}
