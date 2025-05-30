using technical_tests_backend_ssr.Models;

namespace technical_tests_backend_ssr.Repositories
{
    public interface IVentaRepository
    {
        Task<IEnumerable<Venta>> GetAllAsync();
        Task<Venta?> GetByIdAsync(int id);
        Task AddAsync(Venta venta);
        Task UpdateAsync(Venta venta);
        Task DeleteAsync(int id);
        Task<IEnumerable<Venta>> GetAllVentasByClienteId(int clienteId);
  }
}