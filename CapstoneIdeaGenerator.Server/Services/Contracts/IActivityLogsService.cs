using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;

namespace CapstoneIdeaGenerator.Server.Services.Contracts
{
    public interface IActivityLogsService
    {
        Task RecordLogActivity(int adminId, string name, string email, string action, string details);
        Task<IEnumerable<ActivityLogsDTO>> GetAllActivityLogs();
    }
}
