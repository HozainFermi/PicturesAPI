using Application.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface IOrderService
    {
        Task<OrderDto> Create(CreateOrderDto order);
        Task<OrderDto> GetById(Guid orderId);
        Task<List<OrderDto>> GetByUser(Guid userId);
        Task<List<OrderDto>> GetAll();
        Task<OrderDto> Reject(Guid orderId);
        

    }
}
