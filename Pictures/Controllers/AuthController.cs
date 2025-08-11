using Application.DTOs.RefreshToken;
using Application.DTOs.TokenResponse;
using Application.DTOs.Users;
using Application.Extensions;
using Application.ServiceInterfaces;
using Domain.Exceptions;
using Domain.Exceptions.RepoException;
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
        

        public AuthController(IAuthService authService)
        {
            _authService = authService;            
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> Register(UserDto request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _authService.Register(request, cancellationToken);               
                return Ok(user.ToDto());

            }
            catch (UniqueConstraintException ex) { return BadRequest("User with such email address already exists"); }   
            catch (Exception ex) { return BadRequest("Something is wrong"); }
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto,CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.RefreshTokens(refreshTokenRequestDto, cancellationToken);
                if (result is null || result.AccessToken is null || result.RefreshToken is null) { return Unauthorized("Invalid refresh token."); }//опять логинься
                return Ok(result);
            }

            catch (EntityNotFoundException ex) { return Unauthorized("User is not found."); }
            catch (Exception ex) { return Unauthorized("Something is wrong."); }
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.Login(request, cancellationToken);
               
                Response.Cookies.Append("_ck", result.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    //Secure = true,!!! TEST
                    SameSite = SameSiteMode.Strict
                });

                return Ok(result);
            }
            catch(EntityNotFoundException ex) {return BadRequest("User does not exist"); }
            catch(Exception ex) { return BadRequest("Something is wrong"); }
        }
    }
}
