using AdventureWorks.Hosting.AspNetCore;
using AdventureWorks.Logging.Serilog;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.Hosting.Rest;

public class RestApplicationBuilder : AspNetCoreApplicationBuilder
{
    public RestApplicationBuilder(WebApplicationBuilder builder) : base(builder)
    {
    }

    public static RestApplicationBuilder CreateBuilder(string[] args) => new(WebApplication.CreateBuilder(args));

    public override async Task<WebApplication> BuildAsync(ApplicationName applicationName)
    {
        Builder.Services.AddControllers();
        Builder.Services.AddEndpointsApiExplorer();
        Builder.Services.AddSwaggerGen();

        Builder.Services
            .AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        Builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });


        var app = await base.BuildAsync(applicationName);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}