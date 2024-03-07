namespace UNAM.PrimatesApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BillSettings
    {
        [Key]
        public string SettingsId {  get; set; } = Guid.NewGuid().ToString();
        public DateTime RegisterDate { get; set; }
        public DateTime? ExpirationDate { get; set; } = null;
        public string ClientId {  get; set; } = string.Empty;
        public string TokenId {  get; set; } = string.Empty;
    }
}
