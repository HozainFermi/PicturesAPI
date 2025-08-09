using Domain.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.RepoException;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Extensions;

namespace Persistance.RepoImplementation
{
    public class UserRepository : IUserRepository
    {
        PicturesDbContext _dbContext;

        public UserRepository(PicturesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<UserEntity> AddUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Users.AddAsync(user, cancellationToken);
            _dbContext.SaveChanges();

            return result.Entity;
        }

        public async Task<UserEntity> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity? user = await _dbContext.Users.FindAsync(id, cancellationToken);

                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                return user;
            }
            catch (Exception ex) { throw new RepositoryException("Faild to delete user by Id", ex); }

        }

        public async Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity? user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
                if (user == null) { throw new EntityNotFoundException(email, typeof(UserEntity)); }
                return user;
            }
            catch(Exception ex) { throw new RepositoryException("Faild to get user by email", ex); }
        }

        public async Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity? user = await _dbContext.Users.FindAsync(id);
                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                return user; 
            }
            catch(Exception ex) { throw new RepositoryException("Faild to get user by id", ex); }
        }

        public async Task<UserEntity[]> GetByUsernameAsync(string username, PageParams pageParams ,CancellationToken cancellationToken)
        {
            try
            {
                var users = await _dbContext.Users.AsNoTracking().Page(username, pageParams);
                if (users == null) { throw new EntityNotFoundException(username, typeof(UserEntity)); }                                                
                return users;
            }
            catch (Exception ex) { throw new RepositoryException("Faild to get users by username",ex); }
            

            
        }

        public async Task<CartEntity> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                CartEntity? cart = await _dbContext.Carts.Where(c => c.OwnerId == userId).FirstAsync();
                if (cart == null) { throw new EntityNotFoundException(userId, typeof(CartEntity)); }
                return cart;
            }
            catch (Exception ex) { throw new RepositoryException("Faild to get cart by userId", ex); }
        }

        public async Task<OrderEntity[]> GetUserOrdersAsync(Guid id, PageParams pageParams ,CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);
                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }

                OrderEntity[] orders = user?.Orders?.ToArray() ?? Array.Empty<OrderEntity>();

                return orders;
            
            }
            catch (Exception ex) { throw new RepositoryException("Faild to get user`s orders by userId", ex); }
        }

        public async Task<UserEntity> UpdateBirdthDateAsync(Guid id, DateTime newBirdthdate, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity? user = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (user == null) { throw new EntityNotFoundException(id,typeof(UserEntity)); }
                user.BirthDate = newBirdthdate;
                _dbContext.SaveChanges();
                return user;

            }
            catch (Exception ex) { throw new RepositoryException("Faild to update user's birdthdate", ex); }
        }

        public async Task<UserEntity> UpdateEmailAsync(Guid id, string newEmail, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity? user = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                user.Email= newEmail;
                _dbContext.SaveChanges();
                return user;

            }
            catch (Exception ex) { throw new RepositoryException("Faild to update user's emaill", ex); }
        }

        public async Task<UserEntity> UpdatePasswordAsync(Guid id, string newPassword, CancellationToken cancellationToken)
        {
            try
            {
                UserEntity? user = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                var hashedPassword = new PasswordHasher<UserEntity>().HashPassword(user, newPassword);
                user.PasswordHash = hashedPassword;
                _dbContext.SaveChanges();
                return user;

            }
            catch (Exception ex) { throw new RepositoryException("Faild to update user's birdthdate", ex); }
        }

        public async Task<UserEntity> UpdateRefreshTokenAsync(Guid id, string newRefreshToken, CancellationToken cancellationToken)
        {
            try
            {
               var found = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (found == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                found.RefreshToken = newRefreshToken;
                found.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                _dbContext.SaveChanges();
                return found;

            }
            catch (Exception ex) { throw new RepositoryException("Faild to update user's refresh token", ex); }
        }

        public async Task<UserEntity> UpdateUsernameAsync(Guid id, string newUsername, CancellationToken cancellationToken)
        {
            try
            {
                var found = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (found == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                found.UserName= newUsername;
                _dbContext.SaveChanges();
                return found;

            }
            catch (Exception ex) { throw new RepositoryException("Faild to update user's username", ex); }
        }
    }
}
