using FSLInvesting.App.Services;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Components;

namespace FSLInvesting.App.Pages;

public partial class Home
{
    [Inject] private InquiryService _inquiryService { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    private InquiryModel Inquiry { get; set; } = new();
    private bool IsSuccess { get; set; } = true;

    private async Task HandleForm()
    {
        Console.WriteLine($"Notes submitted: {Inquiry.Notes}");

        var isSuccessStatusCode = await _inquiryService.Create(Inquiry);
        
        if (!isSuccessStatusCode)
        {
            IsSuccess = false;
            return;
        }
        
        _navigationManager.NavigateTo("/inquiry", true); 
      }
}