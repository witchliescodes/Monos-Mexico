namespace UNAM.PrimatesApi.Models
{
    public class AuthModel : Response
    {
        public bool IsAuth { get; set; }
        public string AccesToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;        
        public string Role { get; set; } = string.Empty;
    }
}
