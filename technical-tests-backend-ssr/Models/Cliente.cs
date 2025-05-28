using System.ComponentModel.DataAnnotations;

namespace technical_tests_backend_ssr.Models;

/// <summary>
/// Clase cliente, puede realizar múltiples Compras y solicitar Servicios de PostVenta.
/// </summary>
public class Cliente
{
    /// <summary>
    /// Identificador único del cliente.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Nombre del cliente.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Apellido del cliente.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Apellido { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del cliente.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono del cliente.
    /// </summary>
    [Required]
    [MaxLength(15)]
    public string Telefono { get; set; } = string.Empty;
}