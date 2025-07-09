using Pictures.Application.Models.Pages;

namespace Pictures.Application.DTOs.Carts
{
    public class CartResponseDto
    {
        //public Guid CartId { get; set; }
        //public Guid CartOwnerId { get; set; }
        public PageDto<CartItemPreviewDto> CartItems { get; set; }

    }
}
