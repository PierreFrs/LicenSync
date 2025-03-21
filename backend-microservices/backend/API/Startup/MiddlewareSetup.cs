// <copyright file="MiddlewareSetup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using API.Middleware;
using Core.Entities;
using Prometheus;

namespace API.Startup;

public static class MiddlewareSetup
{
    public static void UseCustomMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseStatusCodePagesWithReExecute("/errors/{0}");

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseHttpMetrics();
        app.UseMetricServer();
        app.UseCookiePolicy(new CookiePolicyOptions { Secure = CookieSecurePolicy.Always });
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<LogEventMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
