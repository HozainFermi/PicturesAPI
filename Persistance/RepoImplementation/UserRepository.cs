using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.RepoException;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Extensions;
using Domain.Models.Pagination;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace Persistance.RepoImplementation
{
    public class UserRepository : IUserRepository
    {
        PicturesDbContext _dbContext;
        ILogger _logger;

        public UserRepository(PicturesDbContext context, ILogger logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<UserEntity> AddUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            if(await _dbContext.Users.AnyAsync(u => u.Email == user.Email))
            {
                throw new UniqueConstraintException(nameof(user.Email), new RepositoryException("User with given emaill already exists"));
            }
            try
            {
                var result = await _dbContext.Users.AddAsync(user, cancellationToken);
                _dbContext.SaveChanges();

                return result.Entity;
            }
            catch (DbException ex) 
            {
                _logger.LogError(ex, "Ошибка при добавлении пользователя {UserId}", user.Id);
                throw new RepositoryException("Database error while deleting", ex);
            }
        }

        public async Task<UserEntity> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
                UserEntity? user = await _dbContext.Users.FindAsync(id, cancellationToken);

                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
            try
            {
                _dbContext.Entry(user).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Ошибка при удалении пользователя {UserId}", id);
                throw new RepositoryException("Database error while deleting", ex);
            }

        }

        public async Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            
                UserEntity? user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
                if (user == null) { throw new EntityNotFoundException(email, typeof(UserEntity)); }
                return user;
            
           // catch(Exception ex) { throw new RepositoryException("Faild to get user by email", ex); }
        }

        public async Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            
                UserEntity? user = await _dbContext.Users.FindAsync(id);
                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                return user; 
            
            //catch(Exception ex) { throw new RepositoryException("Faild to get user by id", ex); }
        }

        public async Task<UserEntity[]> GetByUsernameAsync(string username, PageParams pageParams ,CancellationToken cancellationToken)
        {
            
                var users = await _dbContext.Users.AsNoTracking().Page(username, pageParams);
                if (users == null) { throw new EntityNotFoundException(username, typeof(UserEntity)); }                                                
                return users;
            
            //catch (Exception ex) { throw new RepositoryException("Faild to get users by username",ex); }
            

            
        }

        public async Task<CartEntity> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
           
                CartEntity? cart = await _dbContext.Carts.Where(c => c.OwnerId == userId).FirstAsync();
                if (cart == null) { throw new EntityNotFoundException(userId, typeof(CartEntity)); }
                return cart;
            
            //catch (Exception ex) { throw new RepositoryException("Faild to get cart by userId", ex); }
        }

        public async Task<OrderEntity[]> GetUserOrdersAsync(Guid id, PageParams pageParams ,CancellationToken cancellationToken)
        {
                var user = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);
                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }

            
                OrderEntity[] orders = user?.Orders?.ToArray() ?? Array.Empty<OrderEntity>();

                return orders;
            
            
            //catch (Exception ex) { throw new RepositoryException("Faild to get user`s orders by userId", ex); }
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity updateUser, CancellationToken cancellationToken)
        {
         
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == updateUser.Id, cancellationToken);

            if (existingUser == null)
            {
                throw new EntityNotFoundException(updateUser.Id, typeof(UserEntity));
            }

           
            _dbContext.Entry(existingUser).CurrentValues.SetValues(updateUser);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return existingUser; 
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении пользователя {UserId}", updateUser.Id);
                throw new RepositoryException("Database error while updating", ex);
            }
        }


        public async Task<UserEntity> UpdateBirdthDateAsync(Guid id, DateTime newBirdthdate, CancellationToken cancellationToken)
        {
                UserEntity? user = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (user == null) { throw new EntityNotFoundException(id,typeof(UserEntity)); }
                user.BirthDate = newBirdthdate;
            try
            {
                _dbContext.SaveChanges();
                return user;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении даты рождения пользователя {UserId}", id );
                throw new RepositoryException("Database error while updating", ex);
            }
        }

        public async Task<UserEntity> UpdateEmailAsync(Guid id, string newEmail, CancellationToken cancellationToken)
        {
                UserEntity? user = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                user.Email= newEmail;
            try
            {
                _dbContext.SaveChanges();
                return user;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении email пользователя {UserId}", id);
                throw new RepositoryException("Database error while updating", ex);
            }
        }

        public async Task<UserEntity> UpdatePasswordAsync(Guid id, string newPassword, CancellationToken cancellationToken)
        {
                UserEntity? user = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (user == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                var hashedPassword = new PasswordHasher<UserEntity>().HashPassword(user, newPassword);
                user.PasswordHash = hashedPassword;
            try
            {
                _dbContext.SaveChanges();
                return user;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении пароля пользователя {UserId}", id);
                throw new RepositoryException("Database error while updating", ex);
            }
        }

        public async Task<UserEntity> UpdateRefreshTokenAsync(Guid id, string newRefreshToken, CancellationToken cancellationToken)
        {
               var found = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (found == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                found.RefreshToken = newRefreshToken;
                found.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            try
            {
                _dbContext.SaveChanges();
                return found;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении refresh token пользователя {UserId}", id);
                throw new RepositoryException("Database error while updating", ex);
            }
        }

        public async Task<UserEntity> UpdateUsernameAsync(Guid id, string newUsername, CancellationToken cancellationToken)
        {
                var found = await _dbContext.Users.FindAsync(id, cancellationToken);
                if (found == null) { throw new EntityNotFoundException(id, typeof(UserEntity)); }
                found.UserName= newUsername;
            try
            {
                _dbContext.SaveChanges();
                return found;

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new RepositoryException("Record was changed by another user", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении имени пользователя {UserId}", id);
                throw new RepositoryException("Database error while updating", ex);
            }
        }
        }
    }
}
