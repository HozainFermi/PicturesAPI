using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity> AddUserAsync(UserEntity user, CancellationToken cancellationToken);

        public Task<UserEntity> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        public Task<UserEntity[]> GetByUsernameAsync(string username, PageParams pageParams, CancellationToken cancellationToken);
        public Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<OrderEntity[]> GetUserOrdersAsync(Guid id, PageParams pageParams, CancellationToken cancellationToken);
        public Task<CartEntity> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        public Task<UserEntity> UpdateUsernameAsync(Guid id,string newUsername, CancellationToken cancellationToken);
        public Task<UserEntity> UpdateEmailAsync(Guid id, string newEmail, CancellationToken cancellationToken);
        public Task<UserEntity> UpdatePasswordAsync(Guid id,string newPassword, CancellationToken cancellationToken);
        public Task<UserEntity> UpdateBirdthDateAsync(Guid id,DateTime newBirdthdate, CancellationToken cancellationToken);
        public Task<UserEntity> UpdateRefreshTokenAsync(Guid id, string newRefreshToken, CancellationToken cancellationToken);

        public Task<UserEntity> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
