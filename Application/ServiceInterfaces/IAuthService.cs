using Application.DTOs.TokenResponse;
using Domain.Entities;
using Application.DTOs.Users;
using Application.DTOs.RefreshToken;

namespace Application.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<UserEntity?> Register(UserDto request,CancellationToken cancellationToken);
        Task<TokenResponseDto?> Login(UserLoginDto request, CancellationToken cancellationToken);
        Task<TokenResponseDto?> RefreshTokens(RefreshTokenRequestDto request, CancellationToken cancellationToken);

    }
}
