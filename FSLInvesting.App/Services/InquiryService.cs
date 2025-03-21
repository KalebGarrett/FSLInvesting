using System.Net;
using System.Text;
using System.Text.Json;
using FSLInvesting.Models;
using FSLInvesting.Models.Documents;

namespace FSLInvesting.App.Services;

public class InquiryService
{
    private readonly HttpClient _client;
    private const string Header = "x-api-key";
    private string ApiKey { get; set; } = Environment.GetEnvironmentVariable("ApiKey");

    public InquiryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> Create(InquiryModel inquiry)
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add(Header, ApiKey);

        var json = JsonSerializer.Serialize(inquiry);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("inquiry", content);

        return result.IsSuccessStatusCode;
    }

    public async Task<List<InquiryModel>> Get()
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add(Header, ApiKey);

        var result = await _client.GetAsync("inquiries");

        if (!result.IsSuccessStatusCode)
        {
            return new List<InquiryModel>();
        }

        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            return new List<InquiryModel>();
        }

        var json = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<InquiryModel>>(json);
    }

    public async Task Delete(string id)
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add(Header, ApiKey);
        await _client.DeleteAsync($"inquiry/{id}");
    }
}