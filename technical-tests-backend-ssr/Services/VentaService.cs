using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Repositories;

public class VentaService
{
    private readonly IVentaRepository _ventaRepository;

    public VentaService(IVentaRepository ventaRepository)
    {
        _ventaRepository = ventaRepository;
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
}