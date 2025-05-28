using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

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
    [Required]
    public int ClienteId { get; set; }

    /// <summary>
    /// Identificador del vehículo vendido.
    /// </summary>
    [Required]
    public int VehiculoId { get; set; }

    /// <summary>
    /// Fecha en que se realizó la venta.
    /// </summary>
    [Required]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Monto total de la venta.
    /// </summary>
    [Precision(10, 2)]
    public decimal Total { get; set; }
}
