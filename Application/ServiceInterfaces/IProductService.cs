using IDK.Application.Models.Pages;
using IDK.Application.Models.Products;
using IDK.Application.ProductExtensions;
using IDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDK.Application.Abstractions
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
