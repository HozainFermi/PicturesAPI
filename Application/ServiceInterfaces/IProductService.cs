using Application.Models.Pages;
using Pictures.Application.DTOs.Products;
using Pictures.Application.Models.Pages;
using Pictures.Application.Models.ProductExtensions;

namespace Application.ServiceInterfaces
{
    public interface IProductService
    {
        public Task<ProductCreateDto> Create(ProductCreateDto productDto);
        public Task<ProductDto> DisableById(Guid id);
        public Task<ProductDto> DeleteById(Guid id);
        public Task<PageDto<ProductPreviewDto>> GetProduct(PageParams pageParams, ProductFilter filter, ProductSortParams sort);
        public Task<ProductDto> Update (UpdateProductDto updateProductDto);                     


    }
}
