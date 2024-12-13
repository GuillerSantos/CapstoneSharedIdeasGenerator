using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static System.Net.WebRequestMethods;

namespace CapstoneIdeaGenerator.Client.Pages.UserPages.RatingsPage
{
    public class RatingsBase : ComponentBase
    {
        public List<RatingRequestDTO>? Ratings;
        [Inject] IRatingsService RatingsService { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        public bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadRatings();
        }

        private async Task LoadRatings()
        {
            try
            {
                isLoading = true;
                var response = await RatingsService.GetAllRatings();
                Ratings = response?.ToList() ?? new List<RatingRequestDTO>();

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
            catch (HttpRequestException httpEx)
            {
                Snackbar.Add($"HTTPS Request Error: {httpEx.Message}", Severity.Error);
            }

            StateHasChanged();
        }
    }
}
