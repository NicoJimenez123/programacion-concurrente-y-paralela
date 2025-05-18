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

  public Task<Cliente?> GetClienteByIdAsync(int id) {
    return _clienteRepository.GetByIdAsync(id);
  }

  public async Task<Cliente> AddClienteAsync(Cliente cliente) {
    await _clienteRepository.AddAsync(cliente);
    return cliente;
  }

  public async Task<Cliente> UpdateClienteAsync(Cliente cliente) {
    await _clienteRepository.UpdateAsync(cliente);
    return cliente;
  }

  public async Task<bool> DeleteClienteAsync(int id) {
    var existingCliente = await _clienteRepository.GetByIdAsync(id);
    if (existingCliente == null) return false;

    await _clienteRepository.DeleteAsync(id);
    return true;
  }
  
  public async Task<bool> DeleteClienteAsync(Cliente cliente) {
    var existingCliente = await _clienteRepository.GetByIdAsync(cliente.Id);
    if (existingCliente == null) return false;

    await _clienteRepository.DeleteAsync(cliente.Id);
    return true;
  }
}