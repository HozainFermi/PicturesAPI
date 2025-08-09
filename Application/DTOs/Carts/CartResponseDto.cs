

using Domain.Models;

namespace Application.DTOs.Carts
{
    public class CartResponseDto
    {
        
        public PageDto<CartItemPreviewDto> CartItems { get; set; }

    }
}
