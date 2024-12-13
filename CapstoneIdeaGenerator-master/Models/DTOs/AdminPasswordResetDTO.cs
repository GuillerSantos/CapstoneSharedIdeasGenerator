using System.ComponentModel.DataAnnotations;

namespace CapstoneIdeaGenerator.Client.Models.DTOs
{
    public class AdminPasswordResetDTO
    {
        public string NewPassword { get; set; } = string.Empty;

        [Required, Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
