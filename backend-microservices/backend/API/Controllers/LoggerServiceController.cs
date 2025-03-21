// LoggerServiceController.cs
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[Route("api/logger")]
[ApiController]
public class LoggerServiceController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _eventLoggerServiceUrl;

    public LoggerServiceController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _eventLoggerServiceUrl = configuration.GetSection("EventLoggerServiceURL").Value ?? "";
    }

    /// <summary>
    /// Get all logs in JSON format.
    /// </summary>
    [HttpGet("events")]
    [Authorize("Admin")]
    [SwaggerResponse(200, "Get all logs in JSON format", typeof(string))]
    [SwaggerResponse(204, "No Content")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> GetEvents()
    {
        var response = await _httpClient.GetAsync($"{_eventLoggerServiceUrl}/events");
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content) || content.Contains("\"error\""))
        {
            return NoContent();
        }

        return Ok(content);
    }

    /// <summary>
    /// Get a log by id.
    /// </summary>
    [HttpGet("events/{id}")]
    [Authorize("Admin")]
    [SwaggerResponse(200, "Get a log by id", typeof(string))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> GetEvent(string id)
    {
        var response = await _httpClient.GetAsync($"{_eventLoggerServiceUrl}/events/{id}");
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content) || content.Contains("\"error\""))
        {
            return NotFound();
        }

        return Ok(content);
    }

    /// <summary>
    /// Get a log list by type.
    /// </summary>
    [HttpGet("events/type/{type}")]
    [Authorize("Admin")]
    [SwaggerResponse(200, "Get a log list by type", typeof(string))]
    [SwaggerResponse(204, "No Content")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> GetEventsByType(string type)
    {
        var response = await _httpClient.GetAsync($"{_eventLoggerServiceUrl}/events/type/{type}");
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content) || content.Contains("\"error\""))
        {
            return NoContent();
        }

        return Ok(content);
    }

    /// <summary>
    /// Delete a log by id.
    /// </summary>
    [HttpDelete("events/{id}")]
    [Authorize("Admin")]
    [SwaggerResponse(200, "Delete a log by id", typeof(string))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_eventLoggerServiceUrl}/events/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        return Ok();
    }
}
