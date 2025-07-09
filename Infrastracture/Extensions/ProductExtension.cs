using Domain.Entities;
 
using Infrastracture.Extensions;
using System.Linq.Expressions;

namespace Infrastracture.Extensions
{
    public static class ProductExtension
    {

        public static IQueryable<ProductEntity> Filter(this IQueryable<ProductEntity> query, ProductFilter productFilter)
        {
            if (!string.IsNullOrEmpty(productFilter.Name))
            {
                query = query.Where(c => EF.Functions.Like(c.Name, $"{productFilter.Name}%"));
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
                return x => x.Name;
            }
            return orderBy switch
            {
                nameof(ProductEntity.Price) => x => x.Price,
                nameof(ProductEntity.Count) => x => x.Count,
                nameof(ProductEntity.Id) => x => x.Id,
                nameof(ProductEntity.Views) => x => x.Views,
                _ => x => x.Name
            };
        }

        public static async Task<PageDto<ProductPreviewDto>> Page(this IQueryable<ProductEntity> query, PageParams pageParams)
        {

            query.Include(p => p.ProductOwner);

            var total = await query.CountAsync();
            if (total == 0)
            {
                return null;
            }

            var page = pageParams.Page ?? 1;
            var pagesize = pageParams.PageSize ?? 10;

            var skip = (page - 1) * pagesize;
            var res = await query.Skip(skip).Take(pagesize)
            .Select(p => new ProductPreviewDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ShortDescription = p.Description.Length > 40 ? p.Description.Substring(0, 40) + "..." : p.Description,
                ProductImageUrl = p.MediaPathsJson.FirstOrDefault(),

                OwnerName = $"{p.ProductOwner.FirstName} {p.ProductOwner.LastName}",
                OwnerAvatarUrl = p.ProductOwner.MediaPathsJson.FirstOrDefault()
            }
                )
                .ToArrayAsync();

            var pageResult = new PageDto<ProductPreviewDto>(res, total);

            return pageResult;
        }

        public static async Task<PageDto<CartItemPreviewDto>> Page(this IQueryable<CartItemEntity> query, PageParams pageParams)
        {

            query.Include(p => p.Product);

            var total = await query.CountAsync();
            if (total == 0)
            {
                return null;
            }

            var page = pageParams.Page ?? 1;
            var pagesize = pageParams.PageSize ?? 10;

            var skip = (page - 1) * pagesize;
            var res = await query.Skip(skip).Take(pagesize)
            .Select(p => new CartItemPreviewDto
            {
                CartItemId = p.Id,
                CartId = p.CartId,
                MediaPath = p.Product.MediaPathsJson.FirstOrDefault(),
                Title = p.Product.Name,
                Price = p.FinalPrice,
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }
                )
                .ToArrayAsync();

            var pageResult = new PageDto<CartItemPreviewDto>(res, total);

            return pageResult;
        }


    }
}
