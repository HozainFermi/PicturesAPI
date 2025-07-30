using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetById(Guid id);
        public Task<UserEntity> GetByUsername(string username);
        public Task<UserEntity> GetByEmail(string email);
        public Task<List<OrderEntity>> GetUserOrders(Guid id);
        //public Task<CartEntity> GetCartByUserId(Guid userId);

        public Task<UserEntity> UpdateUsername(Guid id,string newusername);
        public Task<UserEntity> UpdateEmail(Guid id, string newemail);
        public Task<UserEntity> UpdatePassword(Guid id,string newpassword);
        public Task<UserEntity> UpdateBirdthDate(Guid id,DateTime newbirdthdate);
        
        public Task<UserEntity> DeleteById(Guid id);

    }
}
