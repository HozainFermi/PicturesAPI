using Pictures.Application.DTOs.Orders;

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
