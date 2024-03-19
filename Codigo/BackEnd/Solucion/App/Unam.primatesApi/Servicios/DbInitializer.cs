using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UNAM.PrimatesApi.Entidades;
using UNAM.PrimatesApi.Interfaces;
using UNAM.PrimatesApi.Models;

namespace UNAM.PrimatesApi.Servicios
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ILogger<DbInitializer> _logger;
        private readonly ConservacionDb _context;
        private readonly UserManager<UserTenant> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ILogger<DbInitializer> logger, ConservacionDb context, UserManager<UserTenant> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitApp()
        {
            await InitRoles();

            await InitAdminuser();
        }

        public async Task InitRoles()
        {
            try
            {
                var roles = await _context.Roles.CountAsync();

                if (roles == 0)
                {
                    var role = new IdentityRole();
                    role.Name = UserRoles.Admin;
                    await _roleManager.CreateAsync(role);

                    role = new IdentityRole();
                    role.Name = UserRoles.UserTenant;
                    await _roleManager.CreateAsync(role);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task InitAdminuser()
        {
            try
            {
                var users = await _context.Users.CountAsync();

                if (users == 0)
                {
                    UserTenant user = new()
                    {
                        //IdUser = Guid.NewGuid().ToString(),
                        UserName = "Admin",
                        NormalizedUserName = "Admin",
                        Email = "yanelsy@ciencias.unam.mx",
                        NormalizedEmail = "yanelsy@ciencias.unam.mx",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        Active = true
                    };

                    var roleStore = new RoleStore<IdentityRole>(_context);

                    string roleName = UserRoles.Admin;

                    var result = await _userManager.CreateAsync(user, "NoPa$$W0rd");

                    if (result.Succeeded)
                    {
                        var userStore = new UserStore<UserTenant>(_context);

                        await userStore.AddToRoleAsync(user, roleName);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
