using System;

namespace CapstoneIdeaGenerator.Server.Models.DTOs
{
    public class AdminDTO
    {
        public int AdminId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public int Age { get; set; }

        public DateTime? DateJoined { get; set; } = DateTime.UtcNow;

        public string Email { get; set; } = string.Empty;
    }
}
