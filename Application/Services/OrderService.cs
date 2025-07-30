using Application.ServiceInterfaces;
using Pictures.Application.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderDto> Create(CreateOrderDto order)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> GetById(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDto>> GetByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> Reject(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
