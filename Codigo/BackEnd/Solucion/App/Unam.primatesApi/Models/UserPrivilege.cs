using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAM.PrimatesApi.Models
{
    public class UserPrivilege
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        public UserTenant UserTenant { get; set; } = new UserTenant();
        [ForeignKey("RolId")]
        public EnumRole RolId { get; set; }
        public RolTenant Rol { get; set; } = new RolTenant();
        [Required]
        public DateTime RegisterDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool Active { get; set; }
    }
}
