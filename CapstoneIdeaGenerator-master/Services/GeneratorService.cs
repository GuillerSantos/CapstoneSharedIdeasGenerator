using CapstoneIdeaGenerator.Client.Services.Contracts;
using CapstoneIdeaGenerator.Client.Models.DTOs;
using System.Net.Http.Json;

namespace CapstoneIdeaGenerator.Client.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly HttpClient httpClient;

        public GeneratorService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetAllCategories()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<string>>("/api/Generator/categories");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching All Categories: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }

        public async Task<IEnumerable<string>> GetAllProjectTypes()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<string>>("/api/Generator/projectTypes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching All ProjectTypes: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }

        public async Task<CapstonesDTO> GetByProjectTypeAndCategory(string category, string projectType)
        {
            try
            {
                var encodedCategory = Uri.EscapeDataString(category);
                var encodedProjectType = Uri.EscapeDataString(projectType);
                return await httpClient.GetFromJsonAsync<CapstonesDTO>($"/api/Generator/idea/{category}/{projectType}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching Capstone By Project Type and Category: {ex.Message}");
                return null!;
            }
        }

    }
}
