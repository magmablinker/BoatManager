using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using OWT.BoatManager.Api.Common;
using OWT.BoatManager.Api.Options;
using OWT.BoatManager.Api.Security;
using OWT.BoatManager.Api.UseCases.Boats;
using OWT.BoatManager.Application.Common;
using OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;

namespace OWT.BoatManager.Api;

internal static class WebApplicationBuilderExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplication();
        builder.ConfigureInfrastructure();

        builder.Services.AddHealthChecks()
            .AddDatabaseChecks();

        builder.ConfigureAuth();
        builder.ConfigureApiVersioning();
        builder.Services.AddEndpointsApiExplorer();
        builder.ConfigureSwagger();
        builder.Services.AddCors();
    }

    private static void ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddEfCorePersistence(options =>
            builder.Configuration.Bind("Infrastructure:Persistence", options));
    }

    private static void ConfigureAuth(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(options => builder.Configuration
                .GetSection(AuthenticationOptions.SchemeSection)
                .Bind(options))
            .AddJwtBearer(options =>
            {
                builder.Configuration
                    .GetSection(AuthenticationOptions.JwtSection)
                    .Bind(options);

                options.TokenValidationParameters = new ()
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            var issuer = builder.Configuration.GetRequiredValue<string>($"{AuthenticationOptions.JwtSection}:Authority");
            options.AddPolicy(BoatScopes.Read, policy =>
                policy.Requirements.Add(new HasScopeRequirement(BoatScopes.Read, issuer)));
            options.AddPolicy(BoatScopes.Write, policy =>
                policy.Requirements.Add(new HasScopeRequirement(BoatScopes.Write, issuer)));
        });

        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
    }

    private static void ConfigureApiVersioning(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new (1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    private static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new ()
            {
                Name = "Authorization",
                Description = "JWT token must be provided",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new ()
            {
                {
                    new ()
                    {
                        Reference = new ()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}