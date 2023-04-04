﻿using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApplicationBuilder = AdventureWorks.AspNetCore.ApplicationBuilder;

namespace AdventureWorks.Hosting.Rest
{
    public class RestApplicationBuilder : ApplicationBuilder
    {
        public RestApplicationBuilder(WebApplicationBuilder builder) : base(builder)
        {
        }

        public static RestApplicationBuilder CreateBuilder(string[] args) => new(WebApplication.CreateBuilder(args));

        public override WebApplication Build(string applicationName)
        {
            Builder.Services.AddControllers();
            Builder.Services.AddEndpointsApiExplorer();
            Builder.Services.AddSwaggerGen();

            Builder.Services
                .AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

            Builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });


            var app = base.Build(applicationName);

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
}