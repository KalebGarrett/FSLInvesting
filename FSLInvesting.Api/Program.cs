using FSLInvesting.Api.Authentication;
using FSLInvesting.Api.Repositories;
using FSLInvesting.Api.Repositories.Interfaces;
using FSLInvesting.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddScoped<ApiKeyFilter>();

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(InquiryRepository<>));

builder.Services.AddDbContext<FslInvestingDbContext>(options =>
{
    options.UseSqlServer(Environment.GetEnvironmentVariable("SqlServerConnectionString"));
});

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<FslInvestingDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FSLInvesting API V1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapIdentityApi<IdentityUser>();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();