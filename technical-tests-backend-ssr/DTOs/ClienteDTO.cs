
/// <summary>
/// Clase cliente, puede realizar múltiples Compras y solicitar Servicios de PostVenta.
/// </summary>

public class ClienteDTO {
 public int Id { get; set; }
 public string Nombre { get; set; } = string.Empty;
 public string Apellido { get; set; } = string.Empty;
 public string Email { get; set; } = string.Empty;
 public string Telefono { get; set; } = string.Empty;
}