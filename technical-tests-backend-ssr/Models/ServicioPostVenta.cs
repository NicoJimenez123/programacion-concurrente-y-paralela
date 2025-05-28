using System.ComponentModel.DataAnnotations;

namespace technical_tests_backend_ssr.Models;

/// <summary>
/// Clase ServicioPostVenta, representa un servicio de posventa solicitado por un cliente.
/// </summary>
public class ServicioPostVenta
{
    /// <summary>
    /// Identificador único del servicio de posventa.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador del cliente que solicitó el servicio.
    /// </summary>
    [Required]
    public int ClienteId { get; set; }

    /// <summary>
    /// Tipo de servicio solicitado.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string TipoServicio { get; set; } = string.Empty;

    /// <summary>
    /// Fecha en que se solicitó el servicio.
    /// </summary>
    [Required]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Estado actual del servicio.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Estado { get; set; } = string.Empty;
}
