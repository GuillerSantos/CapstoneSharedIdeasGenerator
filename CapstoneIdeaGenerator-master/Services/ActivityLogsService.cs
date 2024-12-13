using System.Net.Http.Json;
using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;

namespace CapstoneIdeaGenerator.Client.Services
{
    public class ActivityLogsService : IActivityLogsService
    {
        private readonly CustomAuthStateProvider authenticationStateProvider;
        private readonly IAdminService adminService;
        private readonly IActivityLogsService activityLogsService;
        private HttpClient httpClient;

        public ActivityLogsService(CustomAuthStateProvider authenticationStateProvider, IAdminService adminService,  HttpClient httpClient)
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.adminService = adminService;
            this.httpClient = httpClient;
        }


        public async Task LogAdminAction(string action)
        {
            var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var admin = authenticationState.User;

            if (admin.Identity.IsAuthenticated)
            {
                var logout = new { Email = admin.Identity.Name };
                var fetchadmin = await adminService.GetAdminByEmail(logout.Email);

                if (fetchadmin != null)
                {
                    var adminLogs = new ActivityLogsDTO
                    {
                        AdminId = fetchadmin.AdminId,
                        Email = fetchadmin.Email,
                        Name = fetchadmin.Name,
                        Action = action,
                        Details = $"Admin {fetchadmin.Name} Performed The Action: {action}",
                        Timestamp = DateTime.UtcNow
                    };

                    await RecordLogsActivity(adminLogs);
                }
                else
                {
                    Console.WriteLine($"Admin with email {logout.Email} not found.");
                }
            }
        }


        public async Task RecordLogsActivity(ActivityLogsDTO logs)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/ActivityLogs/log", logs);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Activity Log Recorded Successfully");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error Recording Logs: {ex.Message}");
            }
        }


        public async Task<IEnumerable<ActivityLogsDTO>> GetAllActivityLogs()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<ActivityLogsDTO>>("/api/ActivityLogs/getlogs");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching All Logs: {ex.Message}");
                return new List<ActivityLogsDTO>();
            }
        }
    }
}
