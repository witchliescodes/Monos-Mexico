using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UNAM.PrimatesApi.Models;

namespace UNAM.PrimatesApi.Entidades
{
    public class ConservacionDb : IdentityDbContext<UserTenant>
    {
        private readonly ILogger<ConservacionDb> _logger;
        
        public ConservacionDb(DbContextOptions<ConservacionDb> optionsBuilder, ILogger<ConservacionDb> logger) :base(optionsBuilder)
        {
            _logger = logger;
        }

        
        public DbSet<UserPrivilege> Privileges { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<RolTenant> RolTenants { get; set; }
        public DbSet<BillSettings> BillSettings { get; set; }
        public DbSet<BillHistory> BillHistory { get; set; }
    }
}
