using System.ComponentModel.DataAnnotations;

namespace CapstoneIdeaGenerator.Client.Models.DTOs
{
    public class CapstonesDTO
    {
        public int CapstoneId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Categories are required.")]
        public string Categories { get; set; } = "";

        [Required(ErrorMessage = "Created By is required.")]
        public string CreatedBy { get; set; } = "";

        [Required(ErrorMessage = "Programming Languages are required.")]
        public string ProgLanguages { get; set; } = "";

        [Required(ErrorMessage = "Databases are required.")]
        public string Databases { get; set; } = "";

        [Required(ErrorMessage = "Frameworks are required.")]
        public string Frameworks { get; set; } = "";

        [Required(ErrorMessage = "Project Type is required.")]
        public string ProjectType { get; set; } = "";
    }
}
