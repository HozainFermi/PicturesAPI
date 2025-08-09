using Domain.Entities;
using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        public Task<OrderEntity> GetByUserId(Guid id, PageParams pageParams, CancellationToken cancellationToken);       
        public Task<OrderEntity> GetById(Guid id, CancellationToken cancellationToken);

        public Task<OrderEntity> EditOrderQuantityById(Guid id, int newQuantity, CancellationToken cancellationToken);
        public Task<OrderEntity> EditOrderDeliveryById(Guid id, string newDeliveryAddress, CancellationToken cancellationToken);
        
        public Task<OrderEntity> Delete(Guid orderId, CancellationToken cancellationToken);

    }
}
