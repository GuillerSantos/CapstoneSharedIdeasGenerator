namespace CapstoneIdeaGenerator.Client.Models.DTOs
{
    public class AdminGetByEmailDTO
    {
        public int  AdminId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
