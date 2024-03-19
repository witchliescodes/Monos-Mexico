using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UNAM.PrimatesApi.Entidades;
using UNAM.PrimatesApi.Infrastructure;
using UNAM.PrimatesApi.Interfaces;
using UNAM.PrimatesApi.Models;

namespace UNAM.PrimatesApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IDbInitializer _dbInitializer;
        private ILogger _logger;

        public AuthController(IAuthService service, ILogger<AuthController> logger, IDbInitializer dbInitializer)
        {
            _service = service;
            _logger = logger;
            _dbInitializer = dbInitializer;

            _dbInitializer.InitApp().Wait();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var auth = await _service.Authenticate(login);

                return Ok(auth);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("!Error en la autenticación¡, intente mas tarde.");
            }
            
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel reqister)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var register = await _service.Register(reqister);

                return Ok(register);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("¡Se presento un error al registrar al usuario!");
            }
            
        }

        [HttpPost("logout")]
        //[AllowAnonymous]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            // optionally "revoke" JWT token on the server side --> add the current token to a block-list
            // https://github.com/auth0/node-jsonwebtoken/issues/375
            var userName = User.Identity?.Name;
            await _service.Logout(userName);
            return Ok();
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }
            
            request.UserName = User.Identity?.Name;
            request.AccessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");

            var auth = await _service.RefreshToken(request);

            return Ok(auth);
        }

    }
}
