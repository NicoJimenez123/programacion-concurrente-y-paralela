// Un Cliente puede realizar múltiples Compras y solicitar Servicios de Posventa.
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using technical_tests_backend_ssr.Models;

/// <summary>
///  Controlador de Cliente
///  </summary>
[Route("api/[controller]")]
[ApiController]

public class ClienteController : ControllerBase
{
  private readonly ClienteService _clienteService;
  private readonly IMapper _mapper;

  /// <summary>
  /// Constructor del controlador de Cliente
  /// </summary>
  /// <param name="clienteService"></param>
  /// <param name="mapper"></param>
  public ClienteController(ClienteService clienteService, IMapper mapper)
  {
    _clienteService = clienteService;
    _mapper = mapper;
  }

  /// <summary>
  /// Obtener todos los clientes.
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
  {
    var clientes = await _clienteService.GetAllClientesAsync();
    return Ok(_mapper.Map<IEnumerable<ClienteDTO>>(clientes));
  }
}