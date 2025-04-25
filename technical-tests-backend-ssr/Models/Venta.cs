namespace technical_tests_backend_ssr.Models;

/// <summary>
/// Clase Venta, representa una transacción de venta de un vehículo a un cliente.
/// </summary>
public class Venta
{
    /// <summary>
    /// Identificador único de la venta.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador del cliente que realizó la compra.
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Identificador del vehículo vendido.
    /// </summary>
    public int VehiculoId { get; set; }

    /// <summary>
    /// Fecha en que se realizó la venta.
    /// </summary>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Monto total de la venta.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Cliente asociado a esta venta.
    /// </summary>
    public Cliente? Cliente { get; set; }

    /// <summary>
    /// Vehículo asociado a esta venta.
    /// </summary>
    public Vehiculo? Vehiculo { get; set; }
}
