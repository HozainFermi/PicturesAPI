using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Carts
{
    public class UpdateCartItemDto
    {
        public Guid CartItemId { get; set; }
        public int? UpdatedQuantity { get; set; }
        
    }
}
