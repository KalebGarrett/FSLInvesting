using FSLInvesting.App.Services;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Components;

namespace FSLInvesting.App.Pages;

public partial class Leads
{
    [Inject] private InquiryService _inquiryService { get; set; }
    private bool Dense { get; set; }
    private bool Hover { get; set; } = true;
    private bool Striped { get; set; } = true;
    private bool Bordered { get; set; }
    private string SearchString { get; set; } = "";
    private InquiryModel SelectedItem { get; set; }
    private List<InquiryModel> Inquiries { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Inquiries = await _inquiryService.Get();
    }

    private bool FilterFunc(InquiryModel inquiry) => FilterFunc(inquiry, SearchString);

    private bool FilterFunc(InquiryModel inquiry, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (inquiry.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (inquiry.Number.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (inquiry.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }
}