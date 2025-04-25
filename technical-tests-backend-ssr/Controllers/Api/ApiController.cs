using AutoMapper;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controlador generico para saber el estado de la API
/// </summary>
[Route("/")]
[ApiController]
public class ApiController : ControllerBase
{
    /// <summary>1
    /// Obtener el estado de la API
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("API is running");
    }
}