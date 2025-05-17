using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Repositories;

public class ClienteService {
  private readonly IClienteRepository _clienteRepository;

  public ClienteService(IClienteRepository clienteRepository) {
    _clienteRepository = clienteRepository;
  }

  public Task<IEnumerable<Cliente>> GetAllClientesAsync() {
    return _clienteRepository.GetAllAsync();
  }
}