using IDK.Application.Models.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Application.DTOs.Carts
{
    public class CartDto
    {
        public Guid CartId { get; set; }
        public Guid CartOwnerId { get; set; }
        public List<CartItemDto>? CartItems { get; set; }
        
    }
}
