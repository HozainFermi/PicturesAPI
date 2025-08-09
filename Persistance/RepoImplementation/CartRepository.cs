using Domain.Entities;
using Domain.Exceptions.RepoException;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.RepoImplementation
{
    public class CartRepository : ICartRepository
    {
        PicturesDbContext _dbContext;

        public CartRepository(PicturesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<CartEntity> AddCartAsync(CartEntity cart, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Carts.AddAsync(cart, cancellationToken);
            return result.Entity;
        }

        public Task<CartEntity> CleanCartByIdAsync(Guid cartId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CartEntity> GetCartByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CartEntity> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CartItemEntity[]> GetCartItemsByUserIdAsync(Guid userId, PageParams pageParams, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserByCartIdAsync(Guid cartId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
