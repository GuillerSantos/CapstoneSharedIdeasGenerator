using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Entities.Models;

namespace CapstoneIdeaGenerator.Server.Services.Contracts
{
    public interface IGeneratorService
    {
        Task<IEnumerable<string>> GetAllCategories();
        Task<Capstones> GetByProjectTypeAndCategory(string category, string projectType);
        Task<IEnumerable<string>> GetAllProjectTypes();
    }
}
