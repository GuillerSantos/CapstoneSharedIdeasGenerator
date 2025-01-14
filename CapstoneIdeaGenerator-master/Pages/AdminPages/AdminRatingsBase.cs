using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using CapstoneIdeaGenerator.Client.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CapstoneIdeaGenerator.Client.Pages.AdminPages
{
    public class AdminRatingsBase : ComponentBase
    {
        public List<RatingsDTO>? Ratings;
        public bool isLoading = false;
        [Inject] IRatingsService RatingsService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IActivityLogsService independentActivityLogsService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAllRatingsDetailes();
        }

        private async Task LoadAllRatingsDetailes()
        {
            try
            {
                isLoading = true;
                var response = await RatingsService.GetAllRatingsDetailes();
                Ratings = response?.ToList() ?? new List<RatingsDTO>();

                if (response != null)
                {
                    await independentActivityLogsService.LogAdminAction("Viewed Ratings");
                }

                if (response == null)
                {
                    Snackbar.Add("No Ratings Found", Severity.Error);
                }
                else
                {
                    Ratings = response.ToList();
                    isLoading = false;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Exception Error: {ex.Message}", Severity.Error);
                NavigationManager.NavigateTo("/home");
            }

            StateHasChanged();
        }
    }
}
