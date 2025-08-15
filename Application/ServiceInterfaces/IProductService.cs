using Application.DTOs.Products;
using Domain.Models.Pagination;
using Domain.Models.Products;

namespace Application.ServiceInterfaces
{
    public interface IProductService
    {
        public Task<ProductDto> Create(CreateProductRequest productDto);
        public Task<ProductDto> DisableById(Guid id);
        public Task<ProductDto> DeleteById(Guid id);
        public Task<PageDto<ProductPreviewDto>> GetProduct(PageParams pageParams, ProductFilter filter, ProductSortParams sort);
        public Task<ProductDto> Update (UpdateProductRequest updateProductDto);


    }
}
