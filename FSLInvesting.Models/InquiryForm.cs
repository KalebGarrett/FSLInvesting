using System.ComponentModel.DataAnnotations;

namespace FSLInvesting.Models;

public class InquiryForm
{
    [Required] public string Name { get; set; }
    [Required] public string Number { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string MonthlyPurchase { get; set; }
    [Required] public bool MonthlyDeals { get; set; }
    [Required] public string TargetArea { get; set; }
    [Required] public string BuyingRequirement { get; set; }
    public string Note { get; set; }
}