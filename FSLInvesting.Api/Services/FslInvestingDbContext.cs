using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FSLInvesting.Api.Services;

public class FslInvestingDbContext : IdentityDbContext
{
    public FslInvestingDbContext(DbContextOptions options) : base(options)
    {
    }
}