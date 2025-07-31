using Application.DTOs.RefreshToken;
using Application.DTOs.TokenResponse;
using Application.DTOs.Users;
using Application.ServiceInterfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class AuthService() : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ICartService _cartService;
        private readonly HttpContext 
        

        public AuthService(IUserRepository userRepo, IConfiguration configuration, ICartService cartService, HttpContext httpContext)
        {
            
        }


        private Guid cartId;

        public async Task<TokenResponseDto?> Login(UserLoginDto request)
        {
            UserEntity user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) { return null; }
            if (new PasswordHasher<UserEntity>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            TokenResponseDto response = new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),

            };

           

            return response;

        }


        public async Task<UserEntity?> Register(UserDto request)
        {
            if (await context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return null;
            }
            UserEntity userEntity = request.ToEntity();
            //var newcart = cartService.Create(userEntity);

            var hashedPassword = new PasswordHasher<UserEntity>().HashPassword(userEntity, request.Password);
            userEntity.PasswordHash = hashedPassword;

            var added = await context.Users.AddAsync(userEntity);

            Guid cardid = await cartService.Create(added.Entity);

            added.Entity.CartId = cardid;

            await context.SaveChangesAsync();

            return userEntity;
        }

        private async Task<UserEntity?> ValidateRefreshToken(Guid userId, string refreshToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow) { return null; }

            return user;
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        private async Task<string> GenerateAndSaveRefreshTokenAsync(UserEntity user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }


        public async Task<TokenResponseDto?> RefreshTokens(RefreshTokenRequestDto request)
        {

            var user = await ValidateRefreshToken(request.UserId, request.RefreshToken);
            if (user is null) { return null; }
            var cart = context.Carts.Where(x => x.CartOwnerId == request.UserId).FirstOrDefault();
            if (cart != null) { cartId = cart.Id; }

            TokenResponseDto response = new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),

            };

            return response;
        }


        private string? CreateToken(UserEntity user)
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.Name, user.FirstName),
               new Claim(ClaimTypes.Surname, user.LastName),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Role,user.Role),

               new Claim("cart_id", user.CartId.ToString() ?? string.Empty)

           };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                audience: configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds

                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }


    }
}
