using Pictures.Application.DTOs.TokenResponse;
using Domain.Entities;
using Pictures.Application.DTOs.Users;
using Application.DTOs.RefreshToken;

namespace IDK.Application.Abstractions
{
    public interface IAuthService
    {
        Task<UserEntity?> Register(UserDto request);
        Task<TokenResponseDto?> Login(UserLoginDto request);
        Task<TokenResponseDto?> RefreshTokens(RefreshTokenRequestDto request);

    }
}
