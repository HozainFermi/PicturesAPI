using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderEntity: BaseEntity
    {

       
        public decimal FinalPrice { get; set; }
        public Guid BuyerId { get; set; }
        // Основное свойство для хранения в jsonb
        [Column(TypeName = "jsonb")]
        public Dictionary<Guid, int> ProductIdAndQuantity { get; set; } = new();

        public UserEntity Buyer { get; set; }

    }
}
