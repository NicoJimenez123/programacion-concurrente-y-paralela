using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Repositories;
using AutoMapper;

public class VentaService
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IVehiculoRepository _vehiculoRepository;
    private readonly IMapper _mapper;

    public VentaService(IVentaRepository ventaRepository, IClienteRepository clienteRepository, IVehiculoRepository vehiculoRepository, IMapper mapper)
    {
        _ventaRepository = ventaRepository;
        _clienteRepository = clienteRepository;
        _vehiculoRepository = vehiculoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Venta>> GetAllAsync()
    {
        return await _ventaRepository.GetAllAsync();
    }

    public async Task<Venta?> GetByIdAsync(int id)
    {
        return await _ventaRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Venta venta)
    {
        await _ventaRepository.AddAsync(venta);
    }

    public async Task UpdateAsync(Venta venta)
    {
        await _ventaRepository.UpdateAsync(venta);
    }

    /// <summary>
    /// Elimina una venta por su identificador.
    /// </summary>
    /// <param name="id">El identificador de la venta a eliminar.</param>
    public async Task DeleteAsync(int id)
    {
        await _ventaRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<VentaDTO>> GetAllVentasByClienteId(int clienteId)
    {
        var ventas = await _ventaRepository.GetAllVentasByClienteId(clienteId);
        return ventas.Select(v => new VentaDTO
        {
            Id = v.Id,
            Fecha = v.Fecha,
            Total = v.Total,
            ClienteId = v.ClienteId
        });
    }

    internal async Task<FacturaVentaDTO?> GetFacturaByVentaIdAsync(int id)
    {
        var factura = await _ventaRepository.GetByIdAsync(id);
        if (factura == null)
        {
            return null;
        }

        // Obtengo el nombre del cliente y el vehículo asociado a la venta
        var cliente = _clienteRepository.GetByIdAsync(factura.ClienteId);
        var clienteNombre = cliente.Result != null ? cliente.Result.Nombre : "Cliente no encontrado";
        var vehiculo = _vehiculoRepository.GetByIdAsync(factura.VehiculoId);
        var vehiculoCaracteristicas = vehiculo.Result != null ?
            _mapper.Map<VehiculoCaracteristicasDTO>(vehiculo.Result) : null;


        return new FacturaVentaDTO
        {
            VentaId = factura.Id,
            ClienteNombre = clienteNombre,
            Vehiculo = vehiculoCaracteristicas,
            Total = factura.Total,
            Fecha = factura.Fecha
        };
    }

    /// <summary>
    /// Usando programación paralela, se obtiene la suma total de las ganancias de todas las ventas registradas.
    /// </summary>
    /// <returns>El monto total de las ganancias de todas las ventas.</returns>
    public async Task<decimal> GetGananciasParalelasAllTime()
    {
        // Obtengo todas las ventas y utilizando paralelismo calculo las ganancias
        var ventas = await _ventaRepository.GetAllAsync();
        if (ventas == null || !ventas.Any())
        {
            return 0;
        }
        // Utilizo AsParallel para realizar el cálculo de manera paralela
        // y luego sumo los totales de cada venta
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var ganancias = ventas.AsParallel().Select(v =>
            { return v.Total % 2 == 0 ? v.Total * 2m : v.Total * 0.5m; }
        ).Sum();
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de ejecución (paralelo): {stopwatch.ElapsedMilliseconds} ms");
        return ganancias;
    }

    /// <summary>
    /// Usando programación secuencial, se obtiene la suma total de las ganancias de todas las ventas registradas.
    /// </summary>
    /// <returns>El monto total de las ganancias de todas las ventas.</returns>
    public async Task<decimal> GetGananciasSecuencialesAllTime()
    {
        // Obtengo todas las ventas y utilizando paralelismo calculo las ganancias
        var ventas = await _ventaRepository.GetAllAsync();
        if (ventas == null || !ventas.Any())
        {
            return 0;
        }
        // Utilizo AsParallel para realizar el cálculo de manera paralela
        // y luego sumo los totales de cada venta
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var ganancias = ventas.Select(v =>
            { return v.Total % 2 == 0 ? v.Total * 2m : v.Total * 0.5m; }
        ).Sum();
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de ejecución (secuencial): {stopwatch.ElapsedMilliseconds} ms");
        return ganancias;
    }

    /// <summary>
    /// Simula una operación costosa por cada venta y compara el tiempo de ejecución secuencial vs paralelo.
    /// </summary>
    public async Task<(long secuencialMs, long paraleloMs, decimal resultadoSecuencial, decimal resultadoParalelo)> DemostracionParalelismo()
    {
        var ventas = await _ventaRepository.GetAllAsync();
        if (ventas == null || !ventas.Any())
            return (0, 0, 0, 0);

        // Simula una operación costosa (por ejemplo, espera de 10ms por venta)
        decimal OperacionCostosa(Venta v)
        {
            // Simula trabajo pesado
            System.Threading.Thread.Sleep(10);
            return v.Total * 1.1m;
        }

        // Secuencial
        var swSec = System.Diagnostics.Stopwatch.StartNew();
        var resultadoSecuencial = ventas.Select(OperacionCostosa).Sum();
        swSec.Stop();

        // Paralelo
        var swPar = System.Diagnostics.Stopwatch.StartNew();
        var resultadoParalelo = ventas.AsParallel().Select(OperacionCostosa).Sum();
        swPar.Stop();

        Console.WriteLine($"Tiempo secuencial: {swSec.ElapsedMilliseconds} ms");
        Console.WriteLine($"Tiempo paralelo: {swPar.ElapsedMilliseconds} ms");

        return (swSec.ElapsedMilliseconds, swPar.ElapsedMilliseconds, resultadoSecuencial, resultadoParalelo);
    }

    /// <summary>
    /// Obtener de forma paralela todas las ventas que superen un monto específico.
    /// </summary>
    public async Task<IEnumerable<Venta>> GetVentasByMontoParalelo(decimal monto)
    {
        var ventas = await _ventaRepository.GetAllAsync();
        if (ventas == null || !ventas.Any())
        {
            return Enumerable.Empty<Venta>();
        }

        // Utilizo AsParallel para filtrar las ventas que superen el monto especificado
        var ventasFiltradas = ventas.AsParallel().Where(v => v.Total > monto).ToList();

        return ventasFiltradas;
    }

    /// <summary>
    /// Procesar ventas menores a X monto de forma paralela.
    /// </summary>
    public async Task<string> GetVentasByMontoMenorParalelo(decimal monto)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var ventas = await this.GetAllAsync();

        var tareas = new List<Task>();

        foreach (var venta in ventas)
        {
            // Crear cada Task con Task.Run 
            var tarea = Task.Run(() =>
            {
                Thread.Sleep(10);
                if (venta.Total > 9000000)
                {
                    throw new InvalidOperationException(
                        $"Venta con ID {venta.Id} tiene un monto demasiado alto: {venta.Total}"
                    );
                }
            });

            tareas.Add(tarea);
        }

        Task whenAllTask = Task.WhenAll(tareas);

        try
        {
            // await para dejar que se ejecuten todas las subtareas en paralelo,
            await whenAllTask;
        }
        catch
        {
            // Cuando hacemos await sobre whenAllTask y alguna subtarea falla, el await lanza
            // directamente la primera excepción interna, así que llegamos acá en el catch genérico.
            // Para recuperar todas las excepciones, miramos la propiedad .Exception de la tarea compuesta.
            if (whenAllTask.Exception is AggregateException aggEx)
            {
                foreach (var ex in aggEx.InnerExceptions)
                {
                    Console.WriteLine($"Excepción capturada: {ex.Message}");
                }
                Console.WriteLine("Cantidad de excepciones capturadas: " + aggEx.InnerExceptions.Count);
            }
            else
            {
                // Sólo por si hubiera alguna otra anomalía, imprimiríamos el mensaje:
                Console.WriteLine($"Excepción inesperada sin contenedor Aggregate: {whenAllTask.Exception?.Message}");
            }
        }

        // Una vez que llegamos aquí, todos los items ya fueron procesados.
        Console.WriteLine("Todos los elementos fueron procesados.");
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de procesamiento paralelo: {stopwatch.ElapsedMilliseconds} ms");

        return "Procesamiento paralelo finalizado.";

        /*
            Resultados:
            Tiempo de procesamiento paralelo: 5119 ms
            Tiempo de procesamiento paralelo: 4848 ms
            Tiempo de procesamiento paralelo: 3907 ms
        */
    }
    
    /// <summary>
    /// Procesar ventas menores a X monto de forma paralela.
    /// </summary>
    public async Task<string> GetVentasByMontoMenorSecuencial(decimal monto)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var ventas = await this.GetAllAsync();

        foreach (var venta in ventas)
        {
            try
            {
                Thread.Sleep(10);
                if (venta.Total > 9000000)
                {
                    throw new InvalidOperationException(
                    $"Venta con ID {venta.Id} tiene un monto demasiado alto: {venta.Total}"
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción capturada: {ex.Message}");
            }
        }

        Console.WriteLine("Todos los elementos fueron procesados.");
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de procesamiento secuencial: {stopwatch.ElapsedMilliseconds} ms");

        return"Procesamiento secuencial finalizado.";
        
        /*
            Resultados:
            Tiempo de procesamiento secuencial: 100888 ms
            Tiempo de procesamiento secuencial: 100891 ms
            Tiempo de procesamiento secuencial: 100911 ms
        */
    }
}