using CapstoneIdeaGenerator.Client.Services.Contracts;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using CapstoneIdeaGenerator.Client.Models.DTOs;

namespace CapstoneIdeaGenerator.Client.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AdminService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }


        public async Task<Response> LoginAsync(AdminLoginDTO request)
        {
            try
            {
                var result = await httpClient.PostAsJsonAsync("/api/Admin/login", request);
                var token = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    await localStorage.SetItemAsync("Token", token);
                    await authenticationStateProvider.GetAuthenticationStateAsync();
                    return new Response { IsSuccess = true, Message = "Login successful" };
                }
                else
                {
                    return new Response { IsSuccess = false, Message = "Login failed. Please check your credentials." };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = "An error occurred: " + ex.Message };
            }
        }



        public async Task<AdminDTO> Register(AdminRegisterDTO request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/Admin/register", request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AdminDTO>();
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task RemoveAdmin(string email)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"/api/Admin/remove/{email}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing admin: {ex.Message}");
                throw;
            }
        }
        

        public async Task<AdminGetByEmailDTO?> GetAdminByEmail(string email)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<AdminGetByEmailDTO>($"/api/Admin/getbyemail?email={email}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Fetching Admin By Email: {ex.Message}");
                return null;
            }
        }


        public async Task<Response> ForgotPassword(AdminForgotPasswordDTO request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/Admin/forgot-password", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Response>();
                    return result;
                }

                throw new Exception("Failed To Send Forgot Password Request");
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occurred: " + ex.Message);
            }
        }


        public async Task<string> ResetPassword(AdminPasswordResetDTO request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/Admin/reset-password", request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                throw new Exception("Failed To Send Password Reset Request");
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occurred: " + ex.Message);
            }
        }


        public async Task<IEnumerable<AdminAccountDTO>> GetAllAccountsAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<AdminAccountDTO>>("/api/Admin/accounts");
        }
    }
}
