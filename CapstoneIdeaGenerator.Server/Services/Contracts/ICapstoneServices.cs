using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.DbContext;
using CapstoneIdeaGenerator.Server.Entities.Models;

namespace CapstoneIdeaGenerator.Server.Services.Contracts
{
    public interface ICapstoneServices
    {
        Task<IEnumerable<Capstones>> GetAllCapstones();
        Task<IEnumerable<Capstones>> GetFilteredCapstones(string query);
        Task<Capstones> GetCapstonesById(int id);
        Task<Capstones> AddCapstones(Capstones capstones);
        Task<Capstones> UpdateCapstones(int id, Capstones capstones);
        Task<bool> RemoveCapstones(int id);
    }
}
