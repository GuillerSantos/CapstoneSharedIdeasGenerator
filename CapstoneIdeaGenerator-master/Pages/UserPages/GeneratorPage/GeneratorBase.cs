using CapstoneIdeaGenerator.Client.Components;
using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapstoneIdeaGenerator.Client.Pages.UserPages.GeneratorPage
{
    public class GeneratorBase : ComponentBase
    {
        [Inject] private IGeneratorService generatorService { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private IRatingsService ratingsService { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private CustomAuthStateProvider customAuthStateProvider { get; set; }
        public RatingRequestDTO Rating { get; set; } = new RatingRequestDTO();

        private readonly DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = true };
        public IEnumerable<string> categories { get; private set; } = new List<string>();
        public IEnumerable<string> projectTypes { get; set; } = new List<string>();
        public string? selectedCategory;
        public string? selectedProjectType;
        public CapstonesDTO? generatedCapstone;
        public bool isIdeaGenerated = false;
        public bool isLoading { get; set; } = false;
        public bool isGenerated = false;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                isLoading = true;
                categories = await generatorService.GetAllCategories();
                projectTypes = await generatorService.GetAllProjectTypes();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error Fetching Industries: {ex.Message}", Severity.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        public async Task GenerateIdea()
        {
            if (string.IsNullOrEmpty(selectedCategory))
            {
                Snackbar.Add("Please Select A Industry", Severity.Warning);
                return;
            }

            if (string.IsNullOrEmpty(selectedProjectType))
            {
                Snackbar.Add("Please Select A Project Type", Severity.Warning);
                return;
            }

            if (isIdeaGenerated && selectedCategory == generatedCapstone?.Categories && selectedProjectType == generatedCapstone?.ProjectType)
            {
                return;
            }

            try
            {
                isGenerated = true;

                generatedCapstone = await generatorService.GetByProjectTypeAndCategory(selectedCategory!, selectedProjectType!);

                if (generatedCapstone == null)
                {
                    Snackbar.Add($"No Capstone Ideas Found For {selectedCategory} Industry And {selectedProjectType} Project Type", Severity.Info);
                }
                else
                {
                    isIdeaGenerated = true;
                }
            }
            catch (Exception)
            {
                Snackbar.Add($"No {selectedProjectType} Project Type For this Industry", Severity.Warning);
            }
        }



        public async Task NextIdea()
        {
            try
            {
                isGenerated = true;
                generatedCapstone = await generatorService.GetByProjectTypeAndCategory(selectedCategory!, selectedProjectType!);
                StateHasChanged();
            }
            catch (Exception)
            {
                Snackbar.Add($"No {selectedProjectType} Project Type For this Industry", Severity.Warning);
            }
        }


        public async Task SubmitRating()
        {
            if (string.IsNullOrEmpty(Rating.UserId))
            {
                if (customAuthStateProvider != null)
                {
                    Rating.UserId = customAuthStateProvider.GenerateRandomUserId(8);
                }
                else
                {
                    Snackbar.Add("Authentication state provider is not configured properly.", Severity.Error);
                    return;
                }
            }

            if (Rating.RatingValue < 1 || Rating.RatingValue > 5)
            {
                Snackbar.Add("Rating value must be between 1 and 5.", Severity.Warning);
                return;
            }

            try
            {
                var ratingRequest = new RatingRequestDTO
                {
                    RatingValue = Rating.RatingValue,
                    UserId = Rating.UserId,
                    CapstoneId = generatedCapstone.CapstoneId,
                    Title = generatedCapstone.Title
                };

                bool result = await ratingsService.SubmitRating(ratingRequest);

                if (result)
                {
                    Snackbar.Add("Rating submitted successfully!", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Failed to submit rating. Please try again.", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error Submitting Rating: {ex.Message}", Severity.Error);
            }
        }
    }
}
