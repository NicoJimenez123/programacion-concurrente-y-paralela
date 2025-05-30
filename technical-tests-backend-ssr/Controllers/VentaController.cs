using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using technical_tests_backend_ssr.Models;

[Route("api/[controller]")]
[ApiController]
public class VentaController : ControllerBase
{
    private readonly VentaService _ventaService;
    private readonly IMapper _mapper;

    public VentaController(VentaService ventaService, IMapper mapper)
    {
        _ventaService = ventaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VentaDTO>>> GetAll()
    {
        var ventas = await _ventaService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<VentaDTO>>(ventas));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VentaDTO>> GetById(int id)
    {
        var venta = await _ventaService.GetByIdAsync(id);
        if (venta == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<VentaDTO>(venta));
    }

    [HttpPost]
    public async Task<ActionResult<VentaDTO>> Create(VentaDTO ventaDTO)
    {
        var venta = _mapper.Map<Venta>(ventaDTO);
        await _ventaService.AddAsync(venta);
        return CreatedAtAction(nameof(GetById), new { id = venta.Id }, _mapper.Map<VentaDTO>(venta));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VentaDTO>> Update(int id, VentaDTO ventaDTO)
    {
        var venta = _mapper.Map<Venta>(ventaDTO);
        await _ventaService.UpdateAsync(venta);
        return Ok(_mapper.Map<VentaDTO>(venta));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ventaService.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpGet("{id}/factura")]
    public async Task<ActionResult<FacturaVentaDTO>> GetFactura(int id)
    {
        var factura = await _ventaService.GetFacturaByVentaIdAsync(id);
        if (factura == null)
            return NotFound();
        return Ok(factura);
    }
}