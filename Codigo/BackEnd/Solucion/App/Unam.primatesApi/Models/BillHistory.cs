using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNAM.PrimatesApi.Models
{
    public class BillHistory
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("SettingsId")]
        public string SettingsId { get; set; } = string.Empty;
        public BillSettings BillSettings { get; set; } = new BillSettings();
        [ForeignKey("UserId")]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public UserTenant User {  get; set; } = new UserTenant();
        public DateTime? PayDate { get; set; }
        public Decimal? PayAmount { get; set; }
        public bool Succes {  get; set; }
    }
}
