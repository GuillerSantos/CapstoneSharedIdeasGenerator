using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Models.DTOs;

namespace CapstoneIdeaGenerator.Server.Services.Contracts
{
    public interface IActivityLogsService
    {
        Task RecordLogActivity(int adminId, string name, string email, string action, string details);
        Task<IEnumerable<ActivityLogsDTO>> GetAllActivityLogs();
    }
}
