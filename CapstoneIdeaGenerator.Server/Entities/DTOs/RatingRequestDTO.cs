namespace CapstoneIdeaGenerator.Server.Entities.DTOs
{
    public class RatingRequestDTO
    {
        public int CapstoneId { get; set; }
        public int RatingValue { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
