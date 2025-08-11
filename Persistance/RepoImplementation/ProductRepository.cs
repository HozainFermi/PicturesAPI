using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.RepoException;
using Domain.Models.Pagination;
using Domain.Models.Products;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistance.Data;
using Persistance.Extensions;
using System.Data.Common;


namespace Persistance.RepoImplementation
{
    public class ProductRepository : IProductRepository
    {
        PicturesDbContext _dbContext;
        private readonly ILogger _logger;

        public ProductRepository(PicturesDbContext picturesDbContext ,ILogger logger)
        {
            _dbContext= picturesDbContext;
            _logger = logger;
        }

        public async Task<ProductEntity> AddProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.Products.AddAsync(product);
                return result.Entity;
            }
            catch (Exception ex) { throw new RepositoryException("Faild to add product", ex); }
        }

        public async Task<ProductEntity> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            ProductEntity? foundProduct = await _dbContext.Products.FindAsync(id);
            if (foundProduct != null) { throw new EntityNotFoundException(id, typeof(ProductEntity)); }
            try
            {
            _dbContext.Products.Entry(foundProduct).State = EntityState.Deleted;
            return foundProduct;

            }
            catch(DbException ex) {throw new RepositoryException("Faild to delete product", ex); }
        }

        public async Task<PageDto<ProductPreviewDto>> GetProductAsync(ProductFilter filter, ProductSortParams sortParams, PageParams pageParams, CancellationToken cancellationToken)
        {
          return await _dbContext.Products.Filter(filter).Sort(sortParams).Page(pageParams);
        }

        public async Task<ProductEntity> UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            var existingProduct = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

            if (existingProduct == null)
            {
                throw new EntityNotFoundException(product.Id, typeof(UserEntity));
            }


            _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return existingProduct;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении товара {UserId}", product.Id);
                throw new RepositoryException("Database error while updating product", ex);
            }
        }
    }
}
