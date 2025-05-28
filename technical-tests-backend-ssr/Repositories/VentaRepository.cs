using technical_tests_backend_ssr.Data;
using technical_tests_backend_ssr.Models;
using Microsoft.EntityFrameworkCore;

namespace technical_tests_backend_ssr.Repositories;

public class VentaRepository : IVentaRepository
{
    private readonly AppDbContext _context;

    public VentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Venta>> GetAllAsync()
    {
        return await _context.Ventas.ToListAsync();
    }

    public async Task<Venta?> GetByIdAsync(int id)
    {
        return await _context.Ventas.FindAsync(id);
    }

    public async Task AddAsync(Venta venta)
    {
        await _context.Ventas.AddAsync(venta);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Venta venta)
    {
        _context.Ventas.Update(venta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var venta = await _context.Ventas.FindAsync(id);
        if (venta == null)
        {
            throw new KeyNotFoundException("Venta no encontrada");
        }
        _context.Ventas.Remove(venta);
        await _context.SaveChangesAsync();
    }
}