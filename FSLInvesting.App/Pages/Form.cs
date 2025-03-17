﻿using FSLInvesting.App.Services;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FSLInvesting.App.Pages;

public partial class Form
{
    [Inject] private InquiryService _inquiryService { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] ISnackbar _snackbar { get; set; }
    private InquiryModel Inquiry { get; set; } = new();
    private bool IsError { get; set; }
    private bool IsSuccessful { get; set; }
    private int CurrentYear { get; } = DateTime.UtcNow.ToLocalTime().Year;

    private async Task HandleForm()
    {
        var isSuccessStatusCode = await _inquiryService.Create(Inquiry);

        if (!isSuccessStatusCode)
        {
            IsError = true;
            return;
        }

        IsSuccessful = true;
        StateHasChanged();

        await Task.Delay(TimeSpan.FromSeconds(3));
        _navigationManager.NavigateTo("/inquiry", true);
    }
}