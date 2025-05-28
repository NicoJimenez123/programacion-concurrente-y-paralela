using technical_tests_backend_ssr.Data;
using technical_tests_backend_ssr.Models;
using Microsoft.EntityFrameworkCore;

namespace technical_tests_backend_ssr.Repositories;

public class VehiculoRepository : IVehiculoRepository
{
  private readonly AppDbContext _context;

  public VehiculoRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Vehiculo>> GetAllAsync()
  {
    return await _context.Vehiculos.ToListAsync();
  }

  public async Task<Vehiculo?> GetByIdAsync(int id)
  {
    return await _context.Vehiculos.FindAsync(id);
  }

  public async Task AddAsync(Vehiculo vehiculo)
  {
    await _context.Vehiculos.AddAsync(vehiculo);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateAsync(Vehiculo vehiculo)
  {
    _context.Vehiculos.Update(vehiculo);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    var vehiculo = await _context.Vehiculos.FindAsync(id);
    if (vehiculo == null)
    {
      throw new KeyNotFoundException("Vehiculo no encontrado");
    }
    _context.Vehiculos.Remove(vehiculo);
    await _context.SaveChangesAsync();
  }
}