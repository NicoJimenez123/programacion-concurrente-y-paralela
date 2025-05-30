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
}