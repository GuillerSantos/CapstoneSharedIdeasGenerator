using System.ComponentModel.DataAnnotations;

namespace CapstoneIdeaGenerator.Client.Models.DTOs
{
    public class AdminRegisterDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
