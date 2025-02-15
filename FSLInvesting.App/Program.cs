using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FSLInvesting.App.Services;
using FSLInvesting.App;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("https://fslinvestingapi.azurewebsites.net/index.html")});

builder.Services.AddScoped<InquiryService>();

await builder.Build().RunAsync();