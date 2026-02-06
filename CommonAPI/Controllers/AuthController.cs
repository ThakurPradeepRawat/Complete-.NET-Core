using Common.BAL.Interfaces;
using Common.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Security.Claims;

namespace CommonAPI.Controllers
{
    [Route("/students/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserValidation _UserValidation;
        private readonly IRefreshToken _refreshToken;
        public AuthController(ITokenService tokenService , IUserValidation userValidation , IRefreshToken refreshToken)
        {
            this._tokenService = tokenService;
            _UserValidation = userValidation;
            _refreshToken = refreshToken;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO  login )
        {
            if (login == null)
            {
                return Unauthorized();
            }
            if (_UserValidation.PassWordValidation(login)) {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "101"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("permission", "student.delete")
                };
                var token = _tokenService.GenerateToken(claims);
                var refreshToken = _refreshToken.GenerateRefreshToken();
                _refreshToken.InsertRefreshToken(login.Email, refreshToken);
                Response.Cookies.Append("refreshToken", refreshToken,
                    new CookieOptions
                    {
                        Secure = true,
                        Expires = DateTime.UtcNow.AddDays(1),
                        HttpOnly = true,
                     
                        SameSite = SameSiteMode.None
                    });
                return Ok(new LoginResponseDTO
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddMinutes(30)
                });
            }
            return Unauthorized()
            ;
         
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody]  LoginDTO login)
        {
            if (login == null)
            {
                return Unauthorized();
            }
            _UserValidation.RegisterUser(login);
            
             return Ok();
        }


        [HttpPost("Refresh")]
        public IActionResult RefreshToken()
        {
           var  RefreshToken = Request.Cookies["refreshToken"];

            if (RefreshToken == null)
            {
                return Unauthorized("Refresh Token Missing ");
            }
            bool IsValid = _refreshToken.RefreshTokenValidation(RefreshToken);
            if (!IsValid)
            {
                return Unauthorized("Refresh Token is not Valid");
            }
            var newRefreshToken = _refreshToken.GenerateRefreshToken();
            _refreshToken.UpdateToken(newRefreshToken);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "101"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("permission", "student.delete")
                };
            var token = _tokenService.GenerateToken(claims);
            Response.Cookies.Append("refreshToken", newRefreshToken,
                new CookieOptions
                {
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(1),
                    HttpOnly = true,
                    SameSite = SameSiteMode.None
                });

            return Ok(
            new LoginResponseDTO
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(30)
            });
        }
    }
}
