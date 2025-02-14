using System.ComponentModel.DataAnnotations;
using FSLInvesting.Api.Authentication;
using FSLInvesting.Api.Repositories.Interfaces;
using FSLInvesting.Models;
using Microsoft.AspNetCore.Mvc;

namespace FSLInvesting.Api.Controllers;

[ApiController]
public class InquiryController : ControllerBase
{
    private readonly IMongoRepository<InquiryModel> _inquiryFormRepository;

    public InquiryController(IMongoRepository<InquiryModel> inquiryFormRepository)
    {
        _inquiryFormRepository = inquiryFormRepository;
    }

    [ServiceFilter(typeof(ApiKeyFilter))]
    [HttpGet("Inquiries")]
    public async Task<IActionResult> GetAll([FromHeader(Name = "x-api-key")] [Required] string header)
    {
        var inquiries = _inquiryFormRepository.AsQueryable();

        if (!inquiries.Any()) return NoContent();

        return Ok(inquiries);
    }

    [ServiceFilter(typeof(ApiKeyFilter))]
    [HttpGet("Inquiry/{id}")]
    public async Task<IActionResult> GetAll(string id, [FromHeader(Name = "x-api-key")] [Required] string header)
    {
        var inquiry = _inquiryFormRepository.AsQueryable().Where(x => x.Id == id);

        if (!inquiry.Any()) return NotFound();

        return Ok(inquiry);
    }

    [ServiceFilter(typeof(ApiKeyFilter))]
    [HttpPost("Inquiry")]
    public async Task<IActionResult> Create(InquiryModel inquiry,
        [FromHeader(Name = "x-api-key")] [Required] string header)
    {
        await _inquiryFormRepository.InsertOneAsync(inquiry);
        return Created(inquiry.Id, inquiry);
    }

    [ServiceFilter(typeof(ApiKeyFilter))]
    [HttpPut("Inquiry/{id}")]
    public async Task<IActionResult> Update(string id, InquiryModel inquiry,
        [FromHeader(Name = "x-api-key")] [Required] string header)
    {
        await _inquiryFormRepository.ReplaceOneAsync(id, inquiry);
        return Ok(inquiry);
    }

    [ServiceFilter(typeof(ApiKeyFilter))]
    [HttpDelete("Inquiry/{id}")]
    public async Task<IActionResult> Delete(string id, [FromHeader(Name = "x-api-key")] [Required] string header)
    {
        await _inquiryFormRepository.DeleteOneAsync(id);
        return NoContent();
    }
}