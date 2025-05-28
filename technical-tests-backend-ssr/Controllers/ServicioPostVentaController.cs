using Microsoft.AspNetCore.Mvc;
using technical_tests_backend_ssr.Models;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class ServicioPostVentaController : ControllerBase
{
    private readonly ServicioPostVentaService _servicioPostVentaService;
    private readonly IMapper _mapper;

    public ServicioPostVentaController(ServicioPostVentaService servicioPostVentaService, IMapper mapper)
    {
        _servicioPostVentaService = servicioPostVentaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServicioPostVenta>>> GetAll()
    {
        var servicios = await _servicioPostVentaService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ServicioPostVentaDTO>>(servicios));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServicioPostVenta>> GetById(int id)
    {
        var servicio = await _servicioPostVentaService.GetByIdAsync(id);
        if (servicio == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ServicioPostVentaDTO>(servicio));
    }

    [HttpPost]
    public async Task<ActionResult<ServicioPostVentaDTO>> Create(ServicioPostVentaDTO servicioDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var servicio = _mapper.Map<ServicioPostVenta>(servicioDTO);
        await _servicioPostVentaService.AddAsync(servicio);
        return CreatedAtAction(nameof(GetById), new { id = servicio.Id }, _mapper.Map<ServicioPostVentaDTO>(servicio));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ServicioPostVentaDTO servicioDTO)
    {
        if (id != servicioDTO.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var servicio = _mapper.Map<ServicioPostVenta>(servicioDTO);
        await _servicioPostVentaService.UpdateAsync(servicio);
        return Ok(_mapper.Map<ServicioPostVentaDTO>(servicio));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _servicioPostVentaService.GetByIdAsync(id);
        if (servicio == null) return NotFound();
        await _servicioPostVentaService.DeleteAsync(id);
        return NoContent();
    }
}
