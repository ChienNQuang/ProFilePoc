using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebUI.Filters;
using WebUI.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);


        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        // services.AddOpenApiDocument(configure =>
        // {
        //     configure.Title = "ProFilePOC2 API";
        //     configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
        //     {
        //         Type = OpenApiSecuritySchemeType.ApiKey,
        //         Name = "Authorization",
        //         In = OpenApiSecurityApiKeyLocation.Header,
        //         Description = "Type into the textbox: Bearer {your JWT token}."
        //     });
        //
        //     configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        // });

        return services;
    }
}
