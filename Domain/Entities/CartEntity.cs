using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartEntity:BaseEntity
    {
        public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
        public Guid OwnerId { get; set; }
        public UserEntity CartOwner { get; set; }
       
    }
}
