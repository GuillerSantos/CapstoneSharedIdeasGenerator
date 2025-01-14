using System;

namespace CapstoneIdeaGenerator.Server.Models.DTOs
{
    public class ActivityLogsDTO
    {
        public int AdminId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
    }
}
