using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Models;
using CapstoneIdeaGenerator.Server.Models.DTOs;

namespace CapstoneIdeaGenerator.Server.Services.Contracts
{
    public interface IRatingsService
    {
        Task<bool> SubmitRating(int capstoneId, int ratingValue, string userId, string title);
        Task<IEnumerable<RatingRequestDTO>> GetAllRatings();
        Task<IEnumerable<Ratings>> GetAllRatingsDetailes();
    }
}
