using System.Text;
using System.Text.Json;
using FSLInvesting.Models;

namespace FSLInvesting.App.Services;

public class InquiryService
{
    private readonly HttpClient _client;
    private const string Header = "x-api-key";
    private const string ApiKey = "D2355Ca4-9eED-4C6D-Bc49-D4447028759c";

    public InquiryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> Create(InquiryModel inquiry)
    {
        _client.DefaultRequestHeaders.Add(Header, ApiKey);
        var json = JsonSerializer.Serialize(inquiry);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("Inquiry", content);
        
        return result.IsSuccessStatusCode;
    }

    public async Task<List<InquiryModel>> Get()
    {
        _client.DefaultRequestHeaders.Add(Header, ApiKey);
        var result = await _client.GetAsync("Inquiries");

        if (!result.IsSuccessStatusCode) return new List<InquiryModel>();

        var json = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<InquiryModel>>(json);
    }
}