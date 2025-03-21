// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using API.Startup;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    // Load environment variables
    builder.Configuration.AddEnvironmentVariables();
}
else
{
    //Load .env file
    DotNetEnv.Env.TraversePath().Load();
}

builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.SetMinimumLevel(LogLevel.Information);
});

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var env = app.Services.GetRequiredService<IWebHostEnvironment>();

logger.LogInformation("Starting application...");

await using var scope = app.Services.CreateAsyncScope();
try
{
    await scope.MigrateDatabaseAsync(env);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during database migration.");
}

app.UseCustomMiddlewares(app.Environment);

await app.RunAsync();
