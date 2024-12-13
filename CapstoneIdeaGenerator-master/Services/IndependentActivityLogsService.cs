using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;

namespace CapstoneIdeaGenerator.Client.Services
{
    public class IndependentActivityLogsService : IIndependentActivityLogsService
    {
        private readonly CustomAuthStateProvider authenticationStateProvider;
        private readonly IAdminService adminService;
        private readonly IActivityLogsService activityLogsService;

        public IndependentActivityLogsService(CustomAuthStateProvider authenticationStateProvider, IAdminService adminService, IActivityLogsService activityLogsService)
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.adminService = adminService;
            this.activityLogsService = activityLogsService;
        }

        public ActivityLogsDTO ActivityLogsDTO { get; set; } = new ActivityLogsDTO();



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

                    await activityLogsService.RecordLogsActivity(adminLogs);
                }
                else
                {
                    Console.WriteLine($"Admin with email {logout.Email} not found.");
                }
            }
        }
    }
}
