using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UNAM.PrimatesApi.Models;

namespace UNAM.PrimatesApi.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> Authenticate(LoginModel login);
        Task<Response> Register(RegisterModel register);
        Task<AuthModel> RefreshToken(RefreshTokenRequest request);
        Task Logout(string userName);
    }
}
