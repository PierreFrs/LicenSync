// LogEventMiddleware.cs
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Middleware;

public class LogEventMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<LogEventMiddleware> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LogEventMiddleware(
        RequestDelegate next,
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<LogEventMiddleware> logger,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _next = next;
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        // Check if this is a CRUD operation
        if (
            context.Request.Method == HttpMethods.Post
            || context.Request.Method == HttpMethods.Put
            || context.Request.Method == HttpMethods.Delete
        )
        {
            AppUser? user = null;

            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                // Collect relevant information for logging
                user = await userManager.GetUserAsync(context.User);
                var eventType =
                    $"{context.Request.Method.ToLower()}_{context.Request.Path}".Replace("/", "_");
                var (resourceType, resourceId) = ExtractPathSegments(context);

                var logEvent = CreateLogEvent(user ?? new AppUser(), eventType, resourceType, resourceId);

                // Send the log event to the logging microservice
                var eventLoggerServiceUrl = _configuration
                    .GetSection("EventLoggerServiceURL")
                    .Value;
                var logJson = JsonConvert.SerializeObject(logEvent);
                var content = new StringContent(logJson, Encoding.UTF8, "application/json");

                await _httpClient.PostAsync($"{eventLoggerServiceUrl}/events", content);
            }
            catch (AuthenticationException ex)
            {
                _logger.LogError(
                    ex,
                    "Error while logging event for path {Path}",
                    context.Request.Path
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unexpected error while processing request for user {UserId} at path {Path}",
                    user?.Id ?? "unknown",
                    context.Request.Path
                );
            }
        }
    }

    private static (string resourceType, string resourceId) ExtractPathSegments(HttpContext context)
    {
        var pathSegments = context.Request.Path.Value?.Split("/");
        if (pathSegments == null || pathSegments.Length < 3)
        {
            return ("unknown", "unknown");
        }

        var resourceType = pathSegments[2];
        var resourceId = pathSegments[^1]; // Use ^1 to index from the end

        return (resourceType, resourceId);
    }

    private object CreateLogEvent(
        AppUser user,
        string eventType,
        string resourceType,
        string resourceId
    )
    {
        var userName = $"{user.FirstName} {user.LastName}";
        return new
        {
            event_type = eventType,
            resource_type = resourceType,
            resource_id = resourceId,
            user_id = user.Id,
            timestamp = DateTime.UtcNow.ToString("o"),
            metadata = new { name = userName, email = user.Email },
        };
    }
}
