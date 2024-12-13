using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using CapstoneIdeaGenerator.Client.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.CompilerServices;

namespace CapstoneIdeaGenerator.Client.Pages.AdminPages.AccountPage
{
    public class AccountBase : ComponentBase
    {
        [Inject] IAdminService adminService { get; set; }
        [Inject] IDialogService dialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        [Inject] IActivityLogsService IndependentActivityLogsService { get; set; }

        public ICollection<AdminAccountDTO> Admins { get; set; } = new List<AdminAccountDTO>();
        public static AdminRegisterDTO adminRegister = new AdminRegisterDTO();
        public AdminDTO admin;
        public ActivityLogsDTO adminLogs = new ActivityLogsDTO();
        public AdminLoginDTO adminLoggedIn = new AdminLoginDTO();
        public ShowPasswordUtil showPassword { get; set; } = new ShowPasswordUtil();
        public MudForm form;
        public string message = string.Empty;
        public bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadAccounts();
        }

        public async Task LoadAccounts()
        {
            try
            {
                var response = await adminService.GetAllAccountsAsync();
                Admins = response?.ToList() ?? new List<AdminAccountDTO>();

                if (response == null || !Admins.Any())
                {
                    Snackbar.Add("No Account Found", Severity.Warning);
                }
                else
                {
                    Admins = response.ToList();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Exception Error: {ex.Message}", Severity.Error);
                navigationManager.NavigateTo("/home");
            }
        }

        public async Task RegisterOnClick()
        {
            try
            {
                admin = await adminService.Register(adminRegister);
                Snackbar.Add("Successfully Created Admin Account", Severity.Success);
                await IndependentActivityLogsService.LogAdminAction("Created Account");
                await LoadAccounts();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                if (ex.InnerException != null)
                {
                    errorMessage += $" - {ex.InnerException.Message}";
                }

                Snackbar.Add(errorMessage, Severity.Error);
            }
        }


        public async Task RemoveAccount(string email)
        {
            try
            {
                bool? confirm = await dialogService.ShowMessageBox("Delete Confirmation", "Are You Sure You Want To Delete This Account?", yesText: "Delete", cancelText: "Cancel");
                if (confirm == true)
                {
                    await adminService.RemoveAdmin(email);
                    Snackbar.Add("Account Removed Successfully", Severity.Success);
                    await IndependentActivityLogsService.LogAdminAction("Remove Account");
                    await LoadAccounts();

                    await OnInitializedAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing admin: {ex.Message}");
                Snackbar.Add($"Error removing admin: {ex.Message}", Severity.Error);
            }
        }


        public void ShowPassword()
        {
            showPassword.Toggle();
        }
    }
}
