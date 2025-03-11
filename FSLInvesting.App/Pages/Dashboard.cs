﻿using FSLInvesting.App.Services;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FSLInvesting.App.Pages;

public partial class Dashboard
{
    [Inject] private InquiryService _inquiryService { get; set; }
    private List<InquiryModel> Inquiries { get; set; } = new();
    private double[] Data { get; set; } = [];
    private string[] Labels { get; set; } = [];
    private Position LegendPosition { get; set; } = Position.Right;
    private static double BlueLeads { get; set; }
    private static double GreenLeads { get; set; }
    private static double RedLeads { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        BlueLeads = 0;
        GreenLeads = 0;
        RedLeads = 0;
        await SortLeads();
    }

    private async Task SortLeads()
    {
        Inquiries = await _inquiryService.Get();

        foreach (var inquiry in Inquiries)
        {
            if (inquiry.MonthlyPurchases <= 2)
            {
                BlueLeads++;
            }
            else if (inquiry.MonthlyPurchases is > 2 and <= 5)
            {
                GreenLeads++;
            }
            else
            {
                RedLeads++;
            }
        }

        Labels =
        [
            $"Blue Leads (1-2 Properties): {BlueLeads}",
            $"Green Leads (3-5 Properties): {GreenLeads}",
            $"Red Leads (6+ Properties): {RedLeads}"
        ];

        Data = [BlueLeads, GreenLeads, RedLeads];
    }
}