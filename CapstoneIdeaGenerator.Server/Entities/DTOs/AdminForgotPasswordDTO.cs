using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace CapstoneIdeaGenerator.Server.Entities.DTOs
{
    public class AdminForgotPasswordDTO
    {
        [JsonRequired]
        public string Email { get; set; } = string.Empty;
    }
}
