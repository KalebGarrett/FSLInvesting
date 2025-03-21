using FSLInvesting.App.Services;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace FSLInvesting.App.Pages;

public partial class Login
{
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    private UserLogin UserLogin { get; set; } = new();

    private async Task HandleLogin()
    {
        var authStateProvider = (AuthenticationService)AuthenticationStateProvider;
        var formResult = await authStateProvider.Login(UserLogin);
        if (formResult.Success)
        {
            NavigationManager.NavigateTo("");
        }
        else
        {
            UserLogin.Error = formResult.Errors[0];
            Snackbar.Add(UserLogin.Error, Severity.Error);
        }
    }

    private void NavigateToDashboard()
    {
        NavigationManager.NavigateTo("");
    }

    private bool CheckInputFields()
    {
        return string.IsNullOrWhiteSpace(UserLogin.Email) || string.IsNullOrWhiteSpace(UserLogin.Password);
    }
}