using Domain.Entities;
using Domain.Models.Pagination;
using Domain.Models.Products;

namespace Domain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        public Task<ProductEntity> AddProductAsync(ProductEntity product, CancellationToken cancellationToken);

        public Task<PageDto<ProductPreviewDto>> GetProductAsync(ProductFilter filter, ProductSortParams sortParams, PageParams pageParams ,CancellationToken cancellationToken);        

        public Task<ProductEntity> UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken);

        public Task<ProductEntity> DeleteProductAsync(Guid id, CancellationToken cancellationToken);

    }
}
