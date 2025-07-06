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
        public Task<ICollection<CartItemEntity>> GetCartItemsByUserId(int userId);
        public Task<CartEntity> GetCartById(int id);
        public Task<CartEntity> GetCartByUserId(int userId);

        public Task<CartEntity> CleanCartBy
    }
}
