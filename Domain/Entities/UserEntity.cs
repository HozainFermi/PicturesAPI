using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserEntity: BaseEntity
    {
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени от 2 до 30 символов")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна")]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2025", ErrorMessage = $"Дата должна быть от 1900")]
        [MinimumAge(18, ErrorMessage = "Возраст должен быть ≥ 18 лет")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string PasswordHash { get; set; }
       
        [Column(TypeName = "jsonb")]
        public string[] MediaPathsJson { get; set; } = Array.Empty<string>();

        public Guid? CartId { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public ICollection<OrderEntity>? Orders { get; set; }
        public RoleEntity Role { get; set; }

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
