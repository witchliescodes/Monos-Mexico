using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UNAM.PrimatesApi.Models
{
    public class UserTenant: IdentityUser
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();               
        public DateTime RegisterDate { get; set; }
        public bool Active { get; set; }
        public List<Document> Documents { get; set; } = new List<Document>();
        public List<BillHistory> BillHistories { get; set; } = new List<BillHistory>();
    }
}
