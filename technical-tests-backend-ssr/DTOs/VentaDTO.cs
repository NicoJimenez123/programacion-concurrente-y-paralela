/// <summary>
/// DTO para representar una venta.
/// </summary>
public class VentaDTO
{
    /// <summary>
    /// Identificador único de la venta.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Fecha en la que se realizó la venta.
    /// </summary>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Monto total de la venta.
    /// </summary>
    public decimal MontoTotal { get; set; }

    /// <summary>
    /// Identificador del cliente asociado a la venta.
    /// </summary>
    public int ClienteId { get; set; }
}