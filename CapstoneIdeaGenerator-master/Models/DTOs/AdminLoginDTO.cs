using System.Text.Json.Serialization;

namespace CapstoneIdeaGenerator.Client.Models.DTOs
{
    public class AdminLoginDTO
    {
        [JsonRequired]
        public string Email { get; set; }

        [JsonRequired]
        public string Password { get; set; }
    }
}
