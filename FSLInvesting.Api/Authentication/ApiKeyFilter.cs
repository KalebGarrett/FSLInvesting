using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FSLInvesting.Api.Authentication;

public class ApiKeyFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;
    
    public ApiKeyFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key",
                    out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API key header is missing");
            }
            else
            {
                var apiKey = Environment.GetEnvironmentVariable("ApiKey");
                if (!apiKey.Equals(extractedApiKey))
                {
                    context.Result = new UnauthorizedObjectResult("Invalid API key");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}