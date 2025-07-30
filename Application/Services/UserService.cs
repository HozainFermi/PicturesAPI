using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Pages;
using Application.ServiceInterfaces;
using Domain.RepositoryInterfaces;
using IDK.Application.Abstractions;
using Pictures.Application.DTOs.Carts;
using Pictures.Application.DTOs.Orders;
using Pictures.Application.DTOs.Users;
using Pictures.Application.Models.Pages;
namespace Application.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepo;
        ICartItemRepository _cartItemRepo;
        ICartRepository _cartRepo;

        public UserService(IUserRepository userRepo, ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _userRepo = userRepo;
            _cartItemRepo = cartItemRepository;
            _cartRepo = cartRepository;
        }


        public async Task<UserDto> Delete(Guid userId)
        {
            var user = await _userRepo.DeleteById(userId);
            return new UserDto { UserName = user.UserName, BirthDate = user.BirthDate, Email = user.Email, PasswordHash = user.PasswordHash };
        }

        public Task<PageDto<UserDto>> GetAll(PageParams pageParams)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PageDto<CartItemDto>> GetUserCartItems(PageParams pageParams, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PageDto<OrderDto>> GetUserOrders(PageParams pageParams, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
