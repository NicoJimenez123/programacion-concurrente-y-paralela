using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Repositories;

public class ServicioPostVentaService
{
    private readonly IServicioPostVentaRepository _servicioPostVentaRepository;

    public ServicioPostVentaService(IServicioPostVentaRepository servicioPostVentaRepository)
    {
        _servicioPostVentaRepository = servicioPostVentaRepository;
    }

    public async Task<IEnumerable<ServicioPostVenta>> GetAllAsync()
    {
        return await _servicioPostVentaRepository.GetAllAsync();
    }

    public async Task<ServicioPostVenta?> GetByIdAsync(int id)
    {
        return await _servicioPostVentaRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(ServicioPostVenta servicio)
    {
        await _servicioPostVentaRepository.AddAsync(servicio);
    }

    public async Task UpdateAsync(ServicioPostVenta servicio)
    {
        var existingServicio = await _servicioPostVentaRepository.GetByIdAsync(servicio.Id);
        if (existingServicio == null)
        {
            throw new KeyNotFoundException("Servicio no encontrado");
        }
        await _servicioPostVentaRepository.UpdateAsync(servicio);
    }

    public async Task DeleteAsync(int id)
    {
        await _servicioPostVentaRepository.DeleteAsync(id);
    }
}