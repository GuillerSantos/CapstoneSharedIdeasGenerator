global using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using CapstoneIdeaGenerator.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using CapstoneIdeaGenerator.Client;
using CapstoneIdeaGenerator.Client.Utilities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ICapstoneService, CapstoneService>();
builder.Services.AddScoped<IGeneratorService, GeneratorService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IRatingsService, RatingsService>();
builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddScoped<IActivityLogsService, ActivityLogsService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("") });

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

builder.Services.AddMudServices();
builder.Services.AddOptions();

await builder.Build().RunAsync();
