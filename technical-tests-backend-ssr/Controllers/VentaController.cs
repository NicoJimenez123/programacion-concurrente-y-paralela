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

    [HttpGet("ganancias/totales/paralela")]
    public async Task<ActionResult<decimal>> GetGananciasParalelas()
    {
        var ganancias = await _ventaService.GetGananciasParalelasAllTime();
        return Ok(ganancias);
    }

    [HttpGet("ganancias/totales/secuencial")]
    public async Task<ActionResult<decimal>> GetGananciasSecuenciales()
    {
        var ganancias = await _ventaService.GetGananciasSecuencialesAllTime();
        return Ok(ganancias);
    }

    [HttpGet("demostracion/paralelismo")]
    public async Task<ActionResult<object>> DemostracionParalelismo()
    {
        (long secuencialMs, long paraleloMs, decimal resultadoSecuencial, decimal resultadoParalelo) = await _ventaService.DemostracionParalelismo();
        return Ok(new
        {
            SecuencialMs = secuencialMs,
            ParaleloMs = paraleloMs,
            ResultadoSecuencial = resultadoSecuencial,
            ResultadoParalelo = resultadoParalelo
        });
    }

    // Ventas que superen X monto
    [HttpGet("mayores-a/{monto}")]
    public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentasMayoresA(decimal monto)
    {
        var ventas = await _ventaService.GetVentasByMontoParalelo(monto);
        return Ok(_mapper.Map<IEnumerable<VentaDTO>>(ventas));
    }

    [HttpPost("procesar-ventas")]
    public async Task<IActionResult> ProcesarVentas()
    {
        var ventas = await _ventaService.GetAllAsync();
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        // procesar elementos en paralelo y sin intereses en el resultado
        Parallel.ForEach(ventas, venta =>
        {
            // Procesamiento intensivo por cada venta
            ProcesarVenta(venta);
        });
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de procesamiento paralelo: {stopwatch.ElapsedMilliseconds} ms");

        // Comparar con ventas.AsParallel()
        var stopwatchParallel = System.Diagnostics.Stopwatch.StartNew();
        // transformar y recolectar resultados
        var ventasParalelas = ventas.AsParallel().Select(venta => ProcesarVenta(venta)).ToList();
        stopwatchParallel.Stop();
        Console.WriteLine($"Tiempo de procesamiento paralelo con AsParallel: {stopwatchParallel.ElapsedMilliseconds} ms");

        return Ok("Procesamiento paralelo finalizado.");

        /*
            Resultados:
            Intento 1: Tiempo de procesamiento paralelo: 8171 ms
            Intento 1: Tiempo de procesamiento paralelo con AsParallel: 8515 ms        
            Intento 2: Tiempo de procesamiento paralelo: 7497 ms
            Intento 2: Tiempo de procesamiento paralelo con AsParallel: 8500 ms        
            Intento 3: Tiempo de procesamiento paralelo: 5721 ms
            Intento 3: Tiempo de procesamiento paralelo con AsParallel: 8564 ms        
            Intento 4: Tiempo de procesamiento paralelo: 7392 ms
            Intento 5: Tiempo de procesamiento paralelo con AsParallel: 8594 ms        
            Intento 5: Tiempo de procesamiento paralelo: 8029 ms
            Intento 5: Tiempo de procesamiento paralelo con AsParallel: 8564 ms
        */
    }

    [HttpPost("procesar-ventas/paralelo/{monto}")]
    public async Task<IActionResult> ProcesarVentasParalelo(int monto)
    {
        if (monto <= 0)
        {
            return BadRequest("El monto debe ser mayor que cero.");
        }
        else if (monto > 10000000)
        {
            return BadRequest("El monto no puede ser mayor a 10,000,000.");
        }
        var ventas = await _ventaService.GetVentasByMontoMenorParalelo(monto);
        return Ok(ventas);
    }

    [HttpPost("procesar-ventas/secuencial/{monto}")]
    public async Task<IActionResult> ProcesarVentasSecuencial(int monto)
    {
        if (monto <= 0)
        {
            return BadRequest("El monto debe ser mayor que cero.");
        }
        else if (monto > 10000000)
        {
            return BadRequest("El monto no puede ser mayor a 10,000,000.");
        }
        var ventas = await _ventaService.GetVentasByMontoMenorSecuencial(monto);
        return Ok(ventas);
    }

    private Venta ProcesarVenta(Venta venta)
    {
        // Simula una operación costosa por cada venta
        System.Threading.Thread.Sleep(10); // Simula un procesamiento intensivo
                                           // Aquí podrías agregar lógica adicional para procesar la venta
        return venta; // Retorna la venta procesada si es necesario
    }
}