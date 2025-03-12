using FSLInvesting.App.Pages.Components;
using FSLInvesting.App.Services;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FSLInvesting.App.Pages;

public partial class Leads
{
    [Inject] private InquiryService InquiryService { get; set; }
    [Inject] private IDialogService Dialog { get; set; }
    private bool Dense { get; set; }
    private bool Hover { get; set; } = true;
    private bool Striped { get; set; } = true;
    private bool Bordered { get; set; }
    private string SearchString { get; set; } = "";
    private InquiryModel SelectedItem { get; set; }
    private List<InquiryModel> Inquiries { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        Inquiries = await InquiryService.Get();
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

    private async Task ShowDeleteInquiryDialog(string id)
    {
        var parameters = new DialogParameters<DeleteInquiryDialog>
        {
            { x => x.ContentText, "Do you really want to delete this inquiry? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error },
            { x => x.Id, id}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall};

        var dialog = await Dialog.ShowAsync<DeleteInquiryDialog>("Delete Inquiry", parameters, options);
        var result = await dialog.Result;
        
        if (!result!.Canceled)
        {
            Inquiries = await InquiryService.Get();
        }
    }
}