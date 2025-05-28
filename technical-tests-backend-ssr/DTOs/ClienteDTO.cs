/// <summary>
/// Clase cliente, puede realizar múltiples Compras y solicitar Servicios de PostVenta.
/// </summary>

/// <summary>
/// Clase cliente, puede realizar múltiples Compras y solicitar Servicios de PostVenta.
/// </summary>
public class ClienteDTO
{
  /// <summary>
  /// Obtiene o establece el identificador único del cliente.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Obtiene o establece el nombre del cliente.
  /// </summary>
  public string Nombre { get; set; } = string.Empty;

  /// <summary>
  /// Obtiene o establece el apellido del cliente.
  /// </summary>
  public string Apellido { get; set; } = string.Empty;

  /// <summary>
  /// Obtiene o establece el email del cliente.
  /// </summary>
  public string Email { get; set; } = string.Empty;

  /// <summary>
  /// Obtiene o establece el teléfono del cliente.
  /// </summary>
  public string Telefono { get; set; } = string.Empty;
}