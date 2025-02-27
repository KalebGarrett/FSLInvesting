using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FSLInvesting.App.Services;
using FSLInvesting.App;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("https://fslinvestingapi.azurewebsites.net/index.html")});
builder.Services.AddScoped<InquiryService>();

builder.Services.AddScoped<Snackbar>();

builder.Services.AddMudServices(c =>
{
    c.SnackbarConfiguration.PreventDuplicates = false;
    c.SnackbarConfiguration.MaxDisplayedSnackbars = 5;
});

await builder.Build().RunAsync();