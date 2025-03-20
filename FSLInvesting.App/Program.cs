using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FSLInvesting.App.Services;
using FSLInvesting.App;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("https://fslinvestingapi.azurewebsites.net")});
builder.Services.AddScoped<InquiryService>();

builder.Services.AddScoped<Snackbar>();
builder.Services.AddScoped<DialogService>();

builder.Services.AddMudServices(c =>
{
    c.SnackbarConfiguration.PreventDuplicates = false;
    c.SnackbarConfiguration.MaxDisplayedSnackbars = 5;
});

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();