using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UNAM.PrimatesApi.Entidades;
using UNAM.PrimatesApi.Interfaces;
using UNAM.PrimatesApi.Models;
using System.Security.Claims;
using UNAM.PrimatesApi.Infrastructure;

namespace UNAM.PrimatesApi.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly ConservacionDb _db;
        private readonly UserManager<UserTenant> _userManager;
        private readonly SignInManager<UserTenant> _signInManager;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ILogger _logger;

        public AuthService(ConservacionDb db, ILogger<AuthService> logger, UserManager<UserTenant> userManager, SignInManager<UserTenant> signInManager, IJwtAuthManager jwtAuthManager)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<AuthModel> Authenticate(LoginModel login)
        {
            AuthModel response = new()
            {
                Success = false,
                Message = "¡Falla en la autentificación!, intente mas tarde"
            };

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user != null)
            {
                SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);

                if (signInResult.Succeeded)
                {
                    response.Success = true;
                    response.IsAuth = true;
                    response.Message = string.Empty;

                    var role = await GetUserRole(user.Id);

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role, role)
                    };

                    var jwtResult = _jwtAuthManager.GenerateTokens(user.UserName, claims, DateTime.Now);

                    response.UserName = user.UserName;
                    response.Role = role;
                    response.AccesToken = jwtResult.AccessToken;
                    response.RefreshToken = jwtResult.RefreshToken.TokenString;
                }
            }
            return response;
        }

        private async Task<string> GetUserRole(string id)
        {
            UserTenant user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var userStore = new UserStore<UserTenant>(_db);

                var role = await userStore.GetRolesAsync(user);

                return role.First();
            }

            return string.Empty;
        }

        public async Task<Response> Register(RegisterModel register)
        {
            Response response = new()
            {
                Success = false,
                Message = "¡Falla al registrar usuario intente mas tarde!"
            };

            var user = await _db.Users.AnyAsync(x => x.Email.Equals(register.Email));

            if (!user)
            {
                UserTenant userTenant = new()
                {
                    NormalizedUserName = register.Name,
                    UserName = register.Email,
                    Email = register.Email,
                    EmailConfirmed = false,
                    Id = Guid.NewGuid().ToString(),
                    RegisterDate = DateTime.Now,
                    Active = true
                };

                var roleStore = new RoleStore<IdentityRole>(_db);

                if (string.IsNullOrEmpty(register.Role))
                {
                    register.Role = UserRoles.UserTenant;
                }


                // crea el usuario
                var result = await _userManager.CreateAsync(userTenant, register.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userTenant, register.Role);

                    await _db.SaveChangesAsync();

                    response.Success = true;
                    response.Message = "¡Registro exitoso!";
                }
            }
            else
            {
                response.Message = "El correo ya se encuentra registrado";
            }
            return response;
        }

        public async Task<AuthModel> RefreshToken(RefreshTokenRequest request)
        {
            AuthModel response = new()
            {
                Success = false,
                Message = "¡Error inesperado en la autenticación!"
            };

            var role = await GetUserRole(request.UserName);

            var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, request.AccessToken, DateTime.Now);            

            response.UserName = request.UserName;
            response.Role = role;
            response.AccesToken = jwtResult.AccessToken;
            response.RefreshToken = jwtResult.RefreshToken.TokenString;

            return response;
        }

        public Task Logout(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
