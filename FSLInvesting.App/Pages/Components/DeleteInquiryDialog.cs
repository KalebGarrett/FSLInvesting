using FSLInvesting.App.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FSLInvesting.App.Pages.Components;

public partial class DeleteInquiryDialog
{
    [Inject] private InquiryService InquiryService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; }
    [Parameter] public string ContentText { get; set; }
    [Parameter] public string ButtonText { get; set; }
    [Parameter] public Color Color { get; set; }
    [Parameter] public string Id { get; set; }
    
    private void Cancel() => MudDialog.Cancel();
    
    private async void Delete()
    {
        await InquiryService.Delete(Id);
        Snackbar.Add("Inquiry has been deleted successfully!", Severity.Success);
        MudDialog.Close(DialogResult.Ok(true));
    }
}