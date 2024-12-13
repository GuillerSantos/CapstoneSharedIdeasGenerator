using Blazored.LocalStorage;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace CapstoneIdeaGenerator.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient httpClient;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            this.localStorageService = localStorageService;
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await localStorageService.GetItemAsync<string>("Token");

            var identity = new ClaimsIdentity();
            httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error parsing token: {ex.Message}");
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }


        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }


        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }



        public string GenerateRandomUserId(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<int?> GetLoggedInAdminId()
        {
            var authState = await GetAuthenticationStateAsync();
            var identity = (ClaimsIdentity)authState.User.Identity;

            if (identity != null)
            {
                var adminIdClaim = identity.FindFirst("AdminId");

                if (adminIdClaim != null && int.TryParse(adminIdClaim.Value, out var adminId))
                {
                    return adminId;
                }
            }

            return null;
        }


        public async Task ClearAdminSession()
        {
            await localStorageService.RemoveItemAsync("AdminSession");
        }
    }
}