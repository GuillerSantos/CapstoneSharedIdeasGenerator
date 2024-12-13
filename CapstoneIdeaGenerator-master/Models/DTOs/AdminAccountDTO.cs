using System.Text.Json.Serialization;

namespace CapstoneIdeaGenerator.Client.Models.DTOs
{
    public class AdminAccountDTO
    {
        public int AdminId { get; set; }

        [JsonRequired]
        public string Name { get; set; } = string.Empty;

        [JsonRequired]
        public string Gender { get; set; } = string.Empty;

        [JsonRequired]
        public int Age { get; set; }

        [JsonRequired]
        public string Email { get; set; } = string.Empty;

        public DateTime DateJoined { get; set; }
    }
}
