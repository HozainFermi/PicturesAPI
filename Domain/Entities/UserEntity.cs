using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserEntity: BaseEntity
    {
        [Required]        
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public DateTime BirthDate { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; }

        [Column(TypeName = "jsonb")]
        public string[] MediaPathsJson { get; set; } = Array.Empty<string>();

        public Guid? CartId { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public ICollection<OrderEntity>? Orders { get; set; }


        public CartEntity InitializeCart()
        {
            Guid guid = Guid.NewGuid();
            CartId = guid;

            return new CartEntity
            {
                CartItems = new List<CartItemEntity>(),
                CartOwner = this,
                CreatedAt = DateTime.UtcNow,
                Id = guid,
                IsActive = true,
                OwnerId = this.Id                
            };

        }



    }
}
