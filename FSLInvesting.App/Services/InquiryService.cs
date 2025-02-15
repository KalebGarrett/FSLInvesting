using System.Text;
using System.Text.Json;
using FSLInvesting.Models;

namespace FSLInvesting.App.Services;

public class InquiryService
{
    private readonly HttpClient _client;

    public InquiryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> Create(InquiryModel inquiry)
    {
        _client.DefaultRequestHeaders.Add("x-api-key", "D2355Ca4-9eED-4C6D-Bc49-D4447028759c");
        
        var json = JsonSerializer.Serialize(inquiry);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("Inquiry", content);

        return result.IsSuccessStatusCode;
    }
}