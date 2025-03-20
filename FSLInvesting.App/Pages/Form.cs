using FSLInvesting.App.Services;
using FSLInvesting.Models;
using FSLInvesting.Models.Documents;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FSLInvesting.App.Pages;

public partial class Form
{
    [Inject] private InquiryService _inquiryService { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }
    private InquiryModel Inquiry { get; set; } = new();
    private int CurrentYear { get; } = DateTime.UtcNow.ToLocalTime().Year;

    private async Task HandleForm()
    {
        var isSuccessStatusCode = await _inquiryService.Create(Inquiry);

        if (!isSuccessStatusCode)
        {
            Snackbar.Add("Oops, something went wrong. Please refresh and try again.", Severity.Error);
            return;
        }

        Snackbar.Add("Your inquiry was submitted successfully!", Severity.Success);
        StateHasChanged();

        await Task.Delay(TimeSpan.FromSeconds(3));
        Snackbar.Clear();
        _navigationManager.NavigateTo("/inquiry", true);
    }
}