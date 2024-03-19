using System.Text.Json.Serialization;

namespace UNAM.PrimatesApi.Models
{
    public class RefreshTokenRequest
    {   
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
        public string UserName { get; internal set; } =string.Empty;
        public string? AccessToken { get; internal set; } = string.Empty;
    }
}
