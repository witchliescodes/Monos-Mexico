using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAM.PrimatesApi.Models
{
    public class Document
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; } = string.Empty;
        public UserTenant UserTenant { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public byte[] DocPaper { get; set; } = null;
    }
}
