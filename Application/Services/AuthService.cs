using Application.DTOs.RefreshToken;
using Application.DTOs.TokenResponse;
using Application.DTOs.Users;
using Application.Extensions;
using Application.ServiceInterfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
       
        
        

        public AuthService(IUserRepository userRepo, IConfiguration configuration, ICartRepository cartRepository)
        {
            _userRepository = userRepo;
            _cartRepository = cartRepository;
            _configuration = configuration;
            
        }
      


       // private Guid cartId;

        public async Task<TokenResponseDto?> Login(UserLoginDto request, CancellationToken cancellationToken)
        {                       
                //UserEntity user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);//user repo getByEmail
                UserEntity user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
                
                if (new PasswordHasher<UserEntity>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                {
                    return null;
                }

                TokenResponseDto response = new TokenResponseDto
                {
                    AccessToken = CreateToken(user),
                    RefreshToken = await GenerateAndSaveRefreshTokenAsync(user,cancellationToken),

                };
                return response;
            

        }


        public async Task<UserEntity?> Register(UserDto request, CancellationToken cancellationToken)
        {         
            UserEntity userEntity = request.ToEntity();
            CartEntity cart = userEntity.InitializeCart();
     
            var hashedPassword = new PasswordHasher<UserEntity>().HashPassword(userEntity, request.Password);
            userEntity.PasswordHash = hashedPassword;


           var addedUser = _userRepository.AddUserAsync(userEntity, cancellationToken);
           var addedCart =  _cartRepository.AddCartAsync(cart,cancellationToken);
          
            Task.WaitAll(addedUser,addedCart);

            return addedUser.Result;
        }

        private async Task<UserEntity?> ValidateRefreshToken(Guid userId, string refreshToken, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
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


        private async Task<string> GenerateAndSaveRefreshTokenAsync(UserEntity user,CancellationToken cancellationToken)
        {
            var refreshToken = GenerateRefreshToken();           
            await _userRepository.UpdateRefreshTokenAsync(user.Id, refreshToken, cancellationToken);
           
            return refreshToken;
        }


        public async Task<TokenResponseDto?> RefreshTokens(RefreshTokenRequestDto request, CancellationToken cancellationToken)
        {

            var user = await ValidateRefreshToken(request.UserId, request.RefreshToken,cancellationToken);
            if (user is null) { return null; }
            //var cart =  await _userRepository.GetCartByUserIdAsync(request.UserId, cancellationToken);//context.Carts.Where(x => x.CartOwnerId == request.UserId).FirstOrDefault();
           // if (cart != null) { cartId = cart.Id; }

            TokenResponseDto response = new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user, cancellationToken),

            };

            return response;
        }


        private string? CreateToken(UserEntity user)
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Role,user.Role.Name),

               new Claim("cart_id", user.CartId.ToString() ?? string.Empty)

           };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds

                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }


    }
}
