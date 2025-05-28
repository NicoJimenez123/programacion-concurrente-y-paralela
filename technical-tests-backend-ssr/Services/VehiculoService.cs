using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Repositories;

public class VehiculoService
{
    private readonly IVehiculoRepository _vehiculoRepository;

    public VehiculoService(IVehiculoRepository vehiculoRepository)
    {
        _vehiculoRepository = vehiculoRepository;
    }

    public async Task<IEnumerable<Vehiculo>> GetAllAsync()
    {
        return await _vehiculoRepository.GetAllAsync();
    }

    public async Task<Vehiculo?> GetByIdAsync(int id)
    {
        return await _vehiculoRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Vehiculo vehiculo)
    {
        await _vehiculoRepository.AddAsync(vehiculo);
    }

    public async Task UpdateAsync(Vehiculo vehiculo)
    {
        var existingVehiculo = await _vehiculoRepository.GetByIdAsync(vehiculo.Id);
        if (existingVehiculo == null)
        {
            throw new KeyNotFoundException("Vehículo no encontrado");
        }
        await _vehiculoRepository.UpdateAsync(vehiculo);
    }

    public async Task DeleteAsync(int id)
    {
        await _vehiculoRepository.DeleteAsync(id);
    }
}
