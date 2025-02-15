using FSLInvesting.App.Services;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Components;

namespace FSLInvesting.App.Pages;

public partial class Home
{
    [Inject] private InquiryService _inquiryService { get; set; }
    private InquiryModel Inquiry { get; set; } = new();

    private async Task Create()
    {
        Inquiry.Id = Guid.NewGuid().ToString();
        Inquiry.AcceptsMonthlyDeals = true;
        await _inquiryService.Create(Inquiry);
    }
}