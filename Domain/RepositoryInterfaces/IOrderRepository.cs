using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        public Task<OrderEntity> GetByUserId(Guid id);       
        public Task<OrderEntity> GetById(Guid id);

        public Task<OrderEntity> EditOrderQuantityById(Guid id, int newQuantity);
        public Task<OrderEntity> EditOrderDeliveryById(Guid id, string newDeliveryAddress);
        
        public Task<OrderEntity> Delete(Guid orderId);

    }
}
