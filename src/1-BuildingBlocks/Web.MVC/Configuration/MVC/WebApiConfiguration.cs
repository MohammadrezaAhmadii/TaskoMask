﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonoApi.Services.Afrr.Activations.Api.Helpers;
using TaskoMask.BuildingBlocks.Web.MVC.Configuration.Jwt;
using TaskoMask.BuildingBlocks.Web.MVC.Configuration.Metric;
using TaskoMask.BuildingBlocks.Web.MVC.Configuration.Swagger;
using TaskoMask.BuildingBlocks.Web.MVC.Services.AuthenticatedUser;

namespace TaskoMask.BuildingBlocks.Web.MVC.Configuration;

/// <summary>
///
/// </summary>
public static class WebApiConfiguration
{
    /// <summary>
    ///
    /// </summary>
    public static void AddWebApiPreConfigured(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services.AddControllers().WithPreventAutoValidation();

        services.AddSwaggerPreConfigured(options =>
        {
            configuration.GetSection("Swagger").Bind(options);
        });

        services.AddHttpContextAccessor();

        services.AddAuthenticatedUserService();

        services.AddWebServerOptions();

        services.AddJwtAuthentication(configuration);

        services.AddCors();

        services.AddMetrics(configuration);
    }

    /// <summary>
    ///
    /// </summary>
    public static void UseWebApiPreConfigured(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        if (app == null)
            throw new ArgumentNullException(nameof(app));

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseGlobalExceptionHandler();

        app.UseSwaggerPreConfigured();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseMetrics(configuration);

        app.UseAuthentication();

        app.UseAuthorization();
    }

    /// <summary>
    /// Prevent auto validate on model binding
    /// </summary>
    private static void WithPreventAutoValidation(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    /// <summary>
    ///
    /// </summary>
    private static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<HttpGlobalExceptionHandler>();
    }
}
