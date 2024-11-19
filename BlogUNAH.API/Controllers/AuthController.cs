using BlogUNAH.API.Dtos.Auth;
using BlogUNAH.API.Dtos.Common;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogUNAH.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    //[Authorize]     //con esto proteje este controlador, excepto de aquellos endpoints que tengan [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService
            )
        {
            this._authService = authService;
        }

        //cuando viene info en el body se pone Post o put
        [HttpPost("login")]
        [AllowAnonymous]        //usuarios anonimos puedan tener autenticación
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login (LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("register")]
        [AllowAnonymous]        //usuarios anonimos puedan tener autenticación
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Register(RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]        //usuarios anonimos puedan tener autenticación
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> RefreshToken(RefreshTokenDto dto)
        {
            var response = await _authService.RefreshTokenAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

    }
}
