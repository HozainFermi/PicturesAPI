using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Orders
{
    public class OrderDto 
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductOwnerId { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public int Count { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
