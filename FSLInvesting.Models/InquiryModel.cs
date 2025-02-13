using System.ComponentModel.DataAnnotations;
using FSLInvesting.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace FSLInvesting.Models;

[BsonCollection("Inquiries")]
public class InquiryModel : IDocument
{
    [BsonId] public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Version { get; set; }
    public bool Deleted { get; set; }
    [Required] public string Name { get; set; }
    [Required] public int Number { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string MonthlyPurchases { get; set; }
    [Required] public bool AcceptsMonthlyDeals { get; set; }
    [Required] public string TargetAreas { get; set; }
    [Required] public string BuyingRequirements { get; set; }
    public string Note { get; set; }
}