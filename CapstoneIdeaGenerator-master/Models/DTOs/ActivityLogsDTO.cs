namespace CapstoneIdeaGenerator.Client.Models.DTOs
{
    public class ActivityLogsDTO
    {
        public int AdminId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
    }
}
