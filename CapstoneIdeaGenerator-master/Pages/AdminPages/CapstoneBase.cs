using CapstoneIdeaGenerator.Client.Services.Contracts;
using CapstoneIdeaGenerator.Client.Models.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CapstoneIdeaGenerator.Client.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CapstoneIdeaGenerator.Client.Pages.AdminPages
{
    public class CapstoneBase : ComponentBase
    {
        public string SearchQuery { get; set; } = string.Empty;
        public IEnumerable<CapstonesDTO> FilteredCapstones { get; set; } = new List<CapstonesDTO>();
        public ICollection<CapstonesDTO> Capstones { get; private set; } = new List<CapstonesDTO>();
        private readonly DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, NoHeader = true };

        [Inject] ICapstoneService CapstoneService { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IActivityLogsService IndependentActivityLogsService { get; set; }
        public bool isLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadCapstones();
        }

        private async Task LoadCapstones()
        {
            try
            {
                isLoading = true;
                Capstones = (await CapstoneService.GetAllCapstones())?.ToList() ?? new List<CapstonesDTO>();
                FilteredCapstones = Capstones.ToList();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Exception Error: {ex.Message}", Severity.Error);
                NavigationManager.NavigateTo("/home");
            }

            isLoading = false;
            StateHasChanged();
        }


        public async Task SearchCapstones(string query)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(query))
                {
                    var filteredResult = await CapstoneService.GetFilteredCapstones(query);
                    FilteredCapstones = filteredResult?.ToList() ?? new List<CapstonesDTO>();
                }
                else
                {
                    FilteredCapstones = Capstones.ToList();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error Searching Capstones: {ex.Message}", Severity.Error);
            }
        }


        public async Task AddCapstone()
        {
            var parameters = new DialogParameters<EditCapstoneDialog>();

            var dialog = DialogService.Show<EditCapstoneDialog>("Add Capstone", parameters, dialogOptions);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Snackbar.Add("Capstone Added Successfully!", Severity.Success);
                await IndependentActivityLogsService.LogAdminAction("Added Capstone");
                await LoadCapstones();
            }
        }

        public async Task UpdateCapstone(CapstonesDTO capstones)
        {
            var parameters = new DialogParameters { ["capstones"] = capstones };
            var dialog = DialogService.Show<EditCapstoneDialog>("Edit Capstone", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Snackbar.Add("Capstone Updated Successfully!", Severity.Success);
                await IndependentActivityLogsService.LogAdminAction("Updated Capstone");
                await LoadCapstones();
            }
        }

        public async Task RemoveCapstone(int Id)
        {
            bool? confirm = await DialogService.ShowMessageBox("Delete Confirmation", "Are you sure you want to delete this Capstone?", yesText: "Delete", cancelText: "Cancel");

            if (confirm == true)
            {
                try
                {
                    await CapstoneService.RemoveCapstone(Id);
                    Snackbar.Add("Capstone Removed Successfully!", Severity.Success);
                    await IndependentActivityLogsService.LogAdminAction("Remove Capstone");
                    await LoadCapstones();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Error removing capstone: {ex.Message}", Severity.Error);
                }
            }
        }
    }
}
