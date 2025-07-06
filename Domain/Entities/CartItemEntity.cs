using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartItemEntity: BaseEntity
    {
        public Guid CartId { get; set; }
        public CartEntity Cart { get; set; }

        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
