using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.RepoImplementation
{
    public class OrderRepository : IOrderRepository
    {
        public Task<OrderEntity> Delete(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> EditOrderDeliveryById(Guid id, string newDeliveryAddress)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> EditOrderQuantityById(Guid id, int newQuantity)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetByUserId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
