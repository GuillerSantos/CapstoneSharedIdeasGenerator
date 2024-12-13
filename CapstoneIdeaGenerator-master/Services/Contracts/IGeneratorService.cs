using CapstoneIdeaGenerator.Client.Models.DTOs;

namespace CapstoneIdeaGenerator.Client.Services.Contracts
{
    public interface IGeneratorService
    {
        Task<IEnumerable<string>> GetAllCategories();
        Task<IEnumerable<string>> GetAllProjectTypes();
        Task<CapstonesDTO> GetByProjectTypeAndCategory(string category, string projectType);
    }
}
