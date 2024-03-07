using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UNAM.PrimatesApi.Models
{
    public class UserTenant: IdentityUser
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; }
        public bool Active { get; set; }
        public List<Document> Documents { get; set; } = new List<Document>();
        public List<BillHistory> BillHistories { get; set; } = new List<BillHistory>();
    }
}
