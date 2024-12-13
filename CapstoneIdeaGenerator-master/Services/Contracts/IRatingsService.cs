using CapstoneIdeaGenerator.Client.Models.DTOs;

namespace CapstoneIdeaGenerator.Client.Services.Contracts
{
    public interface IRatingsService
    {
        Task<bool> SubmitRating(RatingRequestDTO ratingRequestDTO);
        Task<List<RatingRequestDTO>> GetAllRatings();
        Task<List<RatingsDTO>> GetAllRatingsDetailes();
    }
}
