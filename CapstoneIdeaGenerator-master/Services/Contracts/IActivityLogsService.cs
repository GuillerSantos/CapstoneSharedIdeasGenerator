using CapstoneIdeaGenerator.Client.Models.DTOs;

namespace CapstoneIdeaGenerator.Client.Services.Contracts
{
    public interface IActivityLogsService
    {
        Task LogAdminAction(string action);
        Task RecordLogsActivity(ActivityLogsDTO logs);
        Task<IEnumerable<ActivityLogsDTO>> GetAllActivityLogs();
    }
}
