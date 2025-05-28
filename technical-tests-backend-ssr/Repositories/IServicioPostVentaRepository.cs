using technical_tests_backend_ssr.Models;

namespace technical_tests_backend_ssr.Repositories
{
  public interface IServicioPostVentaRepository
  {
    Task<IEnumerable<ServicioPostVenta>> GetAllAsync();
    Task<ServicioPostVenta?> GetByIdAsync(int id);
    Task AddAsync(ServicioPostVenta servicioPostVenta);
    Task UpdateAsync(ServicioPostVenta servicioPostVenta);
    Task DeleteAsync(int id);
  }
}