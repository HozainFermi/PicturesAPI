using Application.DTOs.Carts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models.Pagination;
using Domain.Models.Products;

namespace Persistance.Extensions
{
    public static class ProductExtension
    {

        public static IQueryable<ProductEntity> Filter(this IQueryable<ProductEntity> query, ProductFilter productFilter)
        {
            if (!string.IsNullOrEmpty(productFilter.Name))
            {
                query = query.Where(c => EF.Functions.Like(c.ProductName, $"{productFilter.Name}%"));
            }
            if (!(productFilter.Id is null))
            {
                query = query.Where(x => x.Id == productFilter.Id);
            }
            if (!(productFilter.OwnerId is null))
            {
                query = query.Where(x => x.ProductOwnerId == productFilter.OwnerId);
            }

            return query;
        }

        public static IQueryable<ProductEntity> Sort(this IQueryable<ProductEntity> query, ProductSortParams sortParams)
        {
            return sortParams.SortDirection == SortDirection.Descending
                ? query.OrderByDescending(GetKeySelector(sortParams.OrderBy))
                : query.OrderBy(GetKeySelector(sortParams.OrderBy));
        }

        private static Expression<Func<ProductEntity, object>> GetKeySelector(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return x => x.ProductName;
            }
            return orderBy switch
            {
                nameof(ProductEntity.Price) => x => x.Price,
                nameof(ProductEntity.RemainingNumber) => x => x.RemainingNumber,
                nameof(ProductEntity.Id) => x => x.Id,               
                _ => x => x.ProductName
            };
        }

        public static async Task<ProductEntity[]> Page(this IQueryable<ProductEntity> query, PageParams pageParams)
        {

            query.Include(p => p.ProductOwner);

            //var total = await query.CountAsync();
            //if (total == 0)
            //{
            //    return null;
            //}

            var page = pageParams.Page ?? 1;
            var pagesize = pageParams.PageSize ?? 10;

            var skip = (page - 1) * pagesize;
            var res = await query.Skip(skip).Take(pagesize)
            //.Select(p => new ProductPreviewDto
            //{
            //    Id = p.Id,
            //    Name = p.ProductName,
            //    Price = p.Price,
            //    ShortDescription = p.ProductDescription.Length > 40 ? p.ProductDescription.Substring(0, 40) + "..." : p.ProductDescription, //TO SERVICE
            //    ProductImageUrl = p.MediaPathsJson.FirstOrDefault(),

            //    OwnerName = $"{p.ProductOwner.UserName}",
            //    OwnerAvatarUrl = p.ProductOwner.MediaPathsJson.FirstOrDefault()
            //}
            //    )
                .ToArrayAsync();

            //var pageResult = new PageDto<ProductPreviewDto>(res, total);

            return res;
        }

        public static async Task<CartItemEntity[]> Page(this IQueryable<CartItemEntity> query, PageParams pageParams)
        {

            query.Include(p => p.Product);

            //var total = await query.CountAsync();
            //if (total == 0)
            //{
            //    return null;
            //}

            var page = pageParams.Page ?? 1;
            var pagesize = pageParams.PageSize ?? 10;

            var skip = (page - 1) * pagesize;
            var res = await query.Skip(skip).Take(pagesize)
            //.Select(p => new CartItemPreviewDto
            //{
            //    CartItemId = p.Id,               
            //    MediaPath = p.Product.MediaPathsJson.FirstOrDefault(),    //TO SERVICE
            //    Title = p.Product.ProductName,
            //    Price = p.UnitPrice,
            //    ProductId = p.ProductId,
            //    Quantity = p.Quantity
            //}
            //    )
                .ToArrayAsync();

            //var pageResult = new PageDto<CartItemPreviewDto>(res, total);

            return res;
        }


    }
}
