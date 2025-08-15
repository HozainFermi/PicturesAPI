using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Extensions
{
    public static class CartItemExtension
    {
        public static async Task<CartItemEntity[]> Page(this IQueryable<CartEntity> query, PageParams pageParams,CancellationToken cancellationToken)
        {
          
         var cart = await query.Include(c => c.CartItems).FirstOrDefaultAsync(cancellationToken);

            if (cart == null) { throw new EntityNotFoundException("Cart not found", typeof(CartEntity)); }


            var page = pageParams.Page ?? 1;
            var pagesize = pageParams.PageSize ?? 10;

            var skip = (page - 1) * pagesize;
            
            var res = await cart.CartItems.AsQueryable().OrderBy(x=>x.Id).Skip(skip).Take(pagesize).ToArrayAsync();
            
            return res ;
        }
    }
}
