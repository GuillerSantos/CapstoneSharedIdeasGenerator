using Blazored.LocalStorage;
using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CapstoneIdeaGenerator.Client.Shared
{
    public partial class AdminLayout
    {
        [Inject] private IAdminService adminService { get; set; }
        [Inject] private IActivityLogsService activityLogsService { get; set; }
        [Inject] private AuthenticationStateProvider authenticationStateProvider { get; set; }
        [Inject] private CustomAuthStateProvider austomAuthStateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ILocalStorageService LocalStorage { get; set; }

        public AdminLoginDTO logout = new AdminLoginDTO();
        public ActivityLogsDTO adminLogs { get; set; } = new ActivityLogsDTO();

        public bool drawerOpen = false;

        public bool isDarkMode;

        protected override async Task OnInitializedAsync()
        {
            drawerOpen = await LocalStorage.GetItemAsync<bool>("drawerState");
        }


        public async Task ToggleDrawer()
        {
            await LocalStorage.SetItemAsync("drawerState", drawerOpen);
            drawerOpen = !drawerOpen;
        }

        MudTheme MyCustomTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = Colors.DeepPurple.Darken2,
                Secondary = Colors.Green.Accent4
            },

            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Darken4,
                Secondary = Colors.Green.Accent4
            }
        };


        public async Task Logout()
        {
            var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var admin = authenticationState.User;

            if (admin.Identity.IsAuthenticated)
            {
                await austomAuthStateProvider.ClearAdminSession();
                navigationManager.NavigateTo("/authentication");
                await activityLogsService.LogAdminAction("Admin Logged Out");

            }
            else
            {
                Console.WriteLine("Admin Is Not Authenticated");
            }
        }
    }
}
