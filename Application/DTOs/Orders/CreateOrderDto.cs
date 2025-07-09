using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Application.DTOs.Orders
{
    public class CreateOrderDto
    {
       
        public Guid BuyerId { get; set; }

        public Dictionary<Guid, int> ProductAndQuantity { get; set; }//!!!

        public decimal FinalPrice { get; set; }

    }
}
