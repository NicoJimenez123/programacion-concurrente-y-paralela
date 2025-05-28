using technical_tests_backend_ssr.Data;
using technical_tests_backend_ssr.Models;
using Microsoft.EntityFrameworkCore;

namespace technical_tests_backend_ssr.Repositories;

public class ServicioPostVentaRepository : IServicioPostVentaRepository
{
    private readonly AppDbContext _context;

    public ServicioPostVentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServicioPostVenta>> GetAllAsync()
    {
        return await _context.ServiciosPostVenta.ToListAsync();
    }

    public async Task<ServicioPostVenta?> GetByIdAsync(int id)
    {
        return await _context.ServiciosPostVenta.FindAsync(id);
    }

    public async Task AddAsync(ServicioPostVenta servicioPostVenta)
    {
        await _context.ServiciosPostVenta.AddAsync(servicioPostVenta);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ServicioPostVenta servicioPostVenta)
    {
        _context.ServiciosPostVenta.Update(servicioPostVenta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var servicioPostVenta = await _context.ServiciosPostVenta.FindAsync(id);
        if (servicioPostVenta == null)
        {
            throw new KeyNotFoundException("Servicio post venta no encontrado");
        }
        _context.ServiciosPostVenta.Remove(servicioPostVenta);
        await _context.SaveChangesAsync();
    }
}
