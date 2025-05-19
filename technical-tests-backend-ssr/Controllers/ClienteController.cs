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

  /// <summary>
  /// Obtener un cliente por su ID.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id}")]
  public async Task<ActionResult<ClienteDTO>> GetById(int id)
  {
    var cliente = await _clienteService.GetClienteByIdAsync(id);
    if (cliente == null) return NotFound();
    return Ok(_mapper.Map<ClienteDTO>(cliente));
  }

  /// <summary>
  /// Agregar un nuevo cliente
  /// </summary>
  /// <param name="clienteDTO"></param>
  /// <returns></returns>
  [HttpPost]
  public async Task<ActionResult<ClienteDTO>> Create(ClienteDTO clienteDTO)
  {
    // FluentValidation se hace automáticamente al verificar ModelState.IsValid.
    if (!ModelState.IsValid) return BadRequest(ModelState);
    // Mapear el DTO a la entidad Cliente y agregarlo a la base de datos.
    var cliente = _mapper.Map<Cliente>(clienteDTO);
    await _clienteService.AddClienteAsync(cliente);
    return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, _mapper.Map<ClienteDTO>(cliente));
  }

  /// <summary>
  /// Actualizar un cliente existente
  /// </summary>
  /// <param name="id"></param>
  /// <param name="clienteDTO"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  public async Task<ActionResult<ClienteDTO>> Update(int id, ClienteDTO clienteDTO)
  {
    if (id != clienteDTO.Id) return BadRequest("El ID del cliente no coincide.");
    if (!ModelState.IsValid) return BadRequest(ModelState);
    // Mapear el DTO a la entidad Cliente y actualizarlo en la base de datos.
    var cliente = _mapper.Map<Cliente>(clienteDTO);
    Console.WriteLine($"Actualizando cliente: {cliente.Id} en el controller con {id}");
    await _clienteService.UpdateClienteAsync(cliente);
    return Ok(_mapper.Map<ClienteDTO>(cliente));
  }

  /// <summary>
  /// Eliminar un cliente por su ID
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var cliente = await _clienteService.GetClienteByIdAsync(id);
    if (cliente == null) return NotFound();
    await _clienteService.DeleteClienteAsync(cliente);
    return NoContent();
  }
}