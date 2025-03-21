// HealthController.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 28/10/2024
// Fichier Modifié le : 28/10/2024
// Code développé pour le projet : API

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HealthController : BaseApiController
{
    [HttpGet]
    [Route("")]
    public IActionResult Get()
    {
        return Ok("API is running");
    }
}
