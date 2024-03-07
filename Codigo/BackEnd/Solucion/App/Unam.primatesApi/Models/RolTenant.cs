using System.ComponentModel.DataAnnotations;

namespace UNAM.PrimatesApi.Models
{
    public enum EnumRole
    {
        Administrator = 1,
        UserTenant = 2
    }

    public class RolTenant
    {
        [Key]
        public EnumRole RolId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Active {  get; set; }
    }
}
