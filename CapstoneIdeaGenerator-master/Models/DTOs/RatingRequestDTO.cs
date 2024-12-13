namespace CapstoneIdeaGenerator.Client.Models.DTOs
{
    public class RatingRequestDTO
    {
        public int CapstoneId { get; set; }
        public string Title { get; set; }
        public int RatingValue { get; set; }
        public string UserId { get; set; }
    }
}
