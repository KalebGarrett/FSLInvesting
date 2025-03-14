using System.Globalization;
using CsvHelper;
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
    [Inject] NavigationManager NavigationManager { get; set; }
    private bool Dense { get; set; }
    private bool Hover { get; set; } = true;
    private bool Striped { get; set; } = true;
    private bool Bordered { get; set; }
    private string SearchString { get; set; } = "";
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
            { x => x.Id, id }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await Dialog.ShowAsync<DeleteInquiryDialog>("Delete Inquiry", parameters, options);
        var result = await dialog.Result;

        if (!result!.Canceled)
        {
            Inquiries = await InquiryService.Get();
        }
    }

    private async Task DownloadCsv()
    {
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        
        await csv.WriteRecordsAsync(Inquiries);
        await writer.FlushAsync();
        memoryStream.Position = 0;

        var fileBytes = memoryStream.ToArray();
        var mimeType = "text/csv";

        var base64 = Convert.ToBase64String(fileBytes);
        var fileUrl = $"data:{mimeType};base64,{base64}";

        NavigationManager.NavigateTo(fileUrl, true);
    }
}