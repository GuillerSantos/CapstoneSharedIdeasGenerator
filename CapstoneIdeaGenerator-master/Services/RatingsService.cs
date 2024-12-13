using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using System.Net.Http;
using System.Net.Http.Json;

namespace CapstoneIdeaGenerator.Client.Services
{
    public class RatingsService : IRatingsService
    {
        private readonly HttpClient httpClient;

        public RatingsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> SubmitRating(RatingRequestDTO ratingRequest)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/Ratings/submit", ratingRequest);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error submitting rating: {ex.Message}");
                return false;
            }
        }


        public async Task<List<RatingRequestDTO>> GetAllRatings()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/Ratings");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<RatingRequestDTO>>() ?? new List<RatingRequestDTO>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error Fetching All Ratings: { ex.Message}" );
                return new List<RatingRequestDTO>();
            }
        }


        public async Task<List<RatingsDTO>> GetAllRatingsDetailes()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/Ratings/allDetailes");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<RatingsDTO>>() ?? new List<RatingsDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching All Ratings: {ex.Message}");
                return new List<RatingsDTO>();
            }

        }
    }
}
