using Domain.Entities;
using Domain.Models.Pagination;

namespace Domain.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        public Task<OrderEntity> AddOrderAsync(OrderEntity order, CancellationToken cancellationToken);

        public Task<OrderEntity[]> GetByUserIdAsync(Guid id, PageParams pageParams, CancellationToken cancellationToken);       
        public Task<OrderEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        public Task<OrderEntity> UpdateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken);
        
        public Task<OrderEntity> UpdateOrderDeliveryByIdAsync(Guid id, string newDeliveryAddress, CancellationToken cancellationToken);
        
        public Task<OrderEntity> DeleteAsync(Guid orderId, CancellationToken cancellationToken);

    }
}
