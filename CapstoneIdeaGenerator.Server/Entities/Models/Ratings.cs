using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneIdeaGenerator.Server.Entities.Models
{
    public class Ratings
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int CapstoneId { get; set; }

        [Range(1, 5)]
        public int RatingValue { get; set; }


        public DateTime RatedOn { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("CapstoneId")]
        public Capstones Capstones { get; set; }
    }
}
