using CapstoneIdeaGenerator.Client.Services.Contracts;
using System.Net.Http.Json;
using CapstoneIdeaGenerator.Client.Models.DTOs;

namespace CapstoneIdeaGenerator.Client.Services
{
    public class CapstoneService : ICapstoneService
    {
        private readonly HttpClient httpClient;

        public CapstoneService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<CapstonesDTO>> GetAllCapstones()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<CapstonesDTO>>("/api/Capstone") ?? new List<CapstonesDTO>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching all capstones: {ex.Message}");
                return new List<CapstonesDTO>();
            }
        }


        public async Task<IEnumerable<CapstonesDTO>> GetFilteredCapstones(string query)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<CapstonesDTO>>($"/api/Capstone/filter?query={query}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error Searching Capstones: {ex.Message}");
                return new List<CapstonesDTO>();
            }
        }


        public async Task<CapstonesDTO> GetCapstoneById(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<CapstonesDTO>($"/api/Capstone/{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching capstone by ID {id}: {ex.Message}");
                return null!;
            }
        }

        public async Task AddCapstone(CapstonesDTO capstones)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/Capstone", capstones);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding capstone: {ex.Message}");
            }
        }

        public async Task UpdateCapstone(int id, CapstonesDTO capstones)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"/api/Capstone/{id}", capstones);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating capstone with ID {id}: {ex.Message}");
            }
        }

        public async Task RemoveCapstone(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"/api/Capstone/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error removing capstone with ID {id}: {ex.Message}");
            }
        }
    }
}
