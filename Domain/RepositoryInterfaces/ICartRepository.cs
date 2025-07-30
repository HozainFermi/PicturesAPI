using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ICartRepository
    {
        public Task<CartEntity> Create(UserEntity user);
        public Task<CartEntity> GetById(Guid id);
        public Task<CartEntity> GetByUserId(Guid userId);
        public Task<CartEntity> Delete(Guid id);

        //public Task<CartDto> EmptyCart(Guid id);

        //public Task<PageDto<CartItemPreviewDto>> GetCartByUserId(PageParams pageParams, Guid userId);
    }
}
