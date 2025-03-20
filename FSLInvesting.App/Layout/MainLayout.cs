using FSLInvesting.App.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace FSLInvesting.App.Layout;

public partial class MainLayout
{
    private int CurrentYear { get; } = DateTime.UtcNow.ToLocalTime().Year;
    private bool DrawerOpen { get; set; } = true;
    [Inject] AuthenticationStateProvider Provider { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    private readonly MudTheme _currentTheme = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#AA8020",
        }
    };

    void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
    }

    private void Logout()
    {
        var authStateProvider = (AuthenticationService)Provider;
        authStateProvider.Logout();
        NavigationManager.NavigateTo("/login");
    }

    private void NavigateToLogin()
    {
        NavigationManager.NavigateTo("/login");
    }
}