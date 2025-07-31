using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Infrastracture.RepoImplementation
{
    public class UserRepository : IUserRepository
    {
        public Task<UserEntity> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> EditBirdthDate(Guid id, DateTime newbirdthdate)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> EditEmail(Guid id, string newemail)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> EditPassword(Guid id, string newpassword)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> EditUsername(Guid id, string newusername)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<CartEntity> GetCartByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderEntity>> GetUserOrders(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
