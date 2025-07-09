using Pictures.Application.DTOs.TokenResponse;
using Domain.Entities;
using Pictures.Application.DTOs.Users;
using Pictures.Application.DTOs.RefreshToken;

namespace Pictures.Application.Models.Abstractions
{
    public interface IAuthService
    {
        Task<UserEntity?> Register(UserDto request);
        Task<TokenResponseDto?> Login(UserLoginDto request);
        Task<TokenResponseDto?> RefreshTokens(RefreshTokenRequestDto request);

    }
}
