using Application.DTOs.Orders;
using Domain.Models.Pagination;

namespace Application.ServiceInterfaces
{
    public interface IOrderService
    {
        Task<OrderDto> Create(CreateOrderDto order);
        Task<OrderDto> GetById(Guid orderId);
        Task<PageDto<OrderDto>> GetByUserId(PageParams pageParams,Guid userId);
        Task<List<OrderDto>> GetAll();
        Task<OrderDto> Reject(Guid orderId);
        

    }
}
