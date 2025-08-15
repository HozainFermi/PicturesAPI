using Application.DTOs.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this UserDto user)
        {
            UserEntity entity = new UserEntity
            {

                Email = user.Email,
                BirthDate = user.BirthDate,
                UserName = user.Username,                
                Role = user.Role


            };
            return entity;
        }

        public static UserDto ToDto(this UserEntity user)
        {
            UserDto dto = new UserDto
            {
                Email = user.Email,
                BirthDate = user.BirthDate,
                Username = user.UserName,
                Role = user.Role

            };
            return dto;

        }
    }
}
