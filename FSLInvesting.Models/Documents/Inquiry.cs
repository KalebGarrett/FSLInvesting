using System.Text.Json.Serialization;
using FSLInvesting.Models.Documents.Interfaces;

namespace FSLInvesting.Models.Documents;

[BsonCollection("Inquiries")]
public class Inquiry : IDocument
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")] public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("version")] public int Version { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("number")] public string Number { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; }

    [JsonPropertyName("monthlyPurchases")] public int MonthlyPurchases { get; set; }

    [JsonPropertyName("acceptsMonthlyDeals")]
    public bool AcceptsMonthlyDeals { get; set; }

    [JsonPropertyName("targetAreas")] public string TargetAreas { get; set; }

    [JsonPropertyName("buyingRequirements")]
    public string BuyingRequirements { get; set; }

    [JsonPropertyName("notes")] public string Notes { get; set; }
}