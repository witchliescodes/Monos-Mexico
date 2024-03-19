using System.ComponentModel.DataAnnotations;

namespace UNAM.PrimatesApi.Models
{
    public enum EnumRole
    {
        Administrator = 1,
        UserTenant = 2
    }

    public class UserRoles
    {        
        public const string Admin = nameof(Admin);
        public const string UserTenant = nameof(UserTenant);
    }
}
