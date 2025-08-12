using Domain.Entities;
using Domain.Models.Pagination;

namespace Domain.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        public Task<OrderEntity[]> GetByUserId(Guid id, PageParams pageParams, CancellationToken cancellationToken);       
        public Task<OrderEntity> GetById(Guid id, CancellationToken cancellationToken);

        public Task<OrderEntity> UpdateOrder(OrderEntity orderEntity, CancellationToken cancellationToken);
        
        public Task<OrderEntity> UpdateOrderDeliveryById(Guid id, string newDeliveryAddress, CancellationToken cancellationToken);
        
        public Task<OrderEntity> Delete(Guid orderId, CancellationToken cancellationToken);

    }
}
