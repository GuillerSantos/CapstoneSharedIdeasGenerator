using CapstoneIdeaGenerator.Server.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CapstoneIdeaGenerator.Server.Entities.Models
{
    public class Capstones
    {
        [Key]
        public int CapstoneId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Categories { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string ProgLanguages { get; set; }

        [Required]
        public string Databases { get; set; }

        [Required]
        public string Frameworks { get; set; }

        [Required]
        public string ProjectType { get; set; }

        [SwaggerIgnore]
        [JsonIgnore]
        public ICollection<Ratings>? Ratings { get; set; } = new List<Ratings>();

    }
}
