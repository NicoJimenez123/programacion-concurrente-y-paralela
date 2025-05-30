using Microsoft.AspNetCore.Mvc;
using technical_tests_backend_ssr.Models;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class VehiculoController : ControllerBase
{
    private readonly VehiculoService _vehiculoService;
    private readonly IMapper _mapper;

    public VehiculoController(VehiculoService vehiculoService, IMapper mapper)
    {
        _vehiculoService = vehiculoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehiculo>>> GetAll()
    {
        var vehiculos = await _vehiculoService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<VehiculoDTO>>(vehiculos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vehiculo>> GetById(int id)
    {
        var vehiculo = await _vehiculoService.GetByIdAsync(id);
        if (vehiculo == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<VehiculoDTO>(vehiculo));
    }

    [HttpPost]
    public async Task<ActionResult<VehiculoDTO>> Create(VehiculoDTO vehiculoDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var vehiculo = _mapper.Map<Vehiculo>(vehiculoDTO);
        await _vehiculoService.AddAsync(vehiculo);
        return CreatedAtAction(nameof(GetById), new { id = vehiculo.Id }, _mapper.Map<VehiculoDTO>(vehiculo));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, VehiculoDTO vehiculoDTO)
    {
        if (id != vehiculoDTO.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var vehiculo = _mapper.Map<Vehiculo>(vehiculoDTO);
        await _vehiculoService.UpdateAsync(vehiculo);
        return Ok(_mapper.Map<VehiculoDTO>(vehiculo));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vehiculo = await _vehiculoService.GetByIdAsync(id);
        if (vehiculo == null) return NotFound();
        await _vehiculoService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/stock")]
    public async Task<ActionResult<int>> GetStockById(int id)
    {
        var stock = await _vehiculoService.GetStockByIdAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock);
    }

    [HttpGet("{id}/caracteristicas")]
    public async Task<ActionResult<VehiculoCaracteristicasDTO>> GetVehiculoCaracteristicas(int id)
    {
        var vehiculo = await _vehiculoService.GetByIdAsync(id);
        if (vehiculo == null)
            return NotFound();

        var dto = _mapper.Map<VehiculoCaracteristicasDTO>(vehiculo);
        return Ok(dto);
    }

    [HttpGet("{id}/precio")]
    public async Task<ActionResult<decimal>> GetPrecioById(int id)
    {
        var vehiculo = await _vehiculoService.GetByIdAsync(id);
        if (vehiculo == null)
            return NotFound();

        return Ok(vehiculo.Precio);
    }
}
