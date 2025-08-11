using Domain.Entities;
using Domain.Models.Pagination;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.RepoImplementation
{
    public class OrderRepository : IOrderRepository
    {
        public Task<OrderEntity> Delete(Guid orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> EditOrderDeliveryById(Guid id, string newDeliveryAddress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> EditOrderQuantityById(Guid id, int newQuantity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetById(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetByUserId(Guid id, PageParams pageParams, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
