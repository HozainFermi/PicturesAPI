using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Application.DTOs.Carts
{
    public class CartItemPreviewDto
    {
        public Guid CartItemId { get; set; }
        
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string MediaPath { get; set; }
        public string Title { get; set; }


    }
}
