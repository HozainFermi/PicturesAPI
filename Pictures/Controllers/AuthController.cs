using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Pictures.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        IAuthService _authService;
        ICartService _cartService;

        public AuthController(IAuthService authService, ICartService cartService)
        {

            _authService = authService;
            _cartService = cartService;
        }



        [HttpPost]
        [Route("register")]

        public async Task<ActionResult<UserDto>> Register(UserDto request)
        {
            var user = await _authService.Register(request);
            if (user == null)
            {
                return BadRequest("User with such email address already exists");
            }

            await _cartService.Create(user);
            return Ok(user.ToDto());
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var result = await _authService.RefreshTokens(refreshTokenRequestDto);
            if (result is null || result.AccessToken is null || result.RefreshToken is null) { return Unauthorized("Invalid refresh token."); }//опять логинься
            return Ok(result);
        }



        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            var result = await _authService.Login(request);
            if (result == null)
            {

                return BadRequest("Something is wrong");

            }

            Response.Cookies.Append("_ck", result.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                //Secure = true,!!! TEST
                SameSite = SameSiteMode.Strict
            });

            return Ok(result);
        }
    }
}
