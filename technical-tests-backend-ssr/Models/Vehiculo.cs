namespace technical_tests_backend_ssr.Models;

/// <summary>
/// Clase Vehiculo, puede ser vendido múltiples veces.
/// </summary>
public class Vehiculo
{
    /// <summary>
    /// Identificador único del vehículo.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Marca del vehículo.
    /// </summary>
    public string Marca { get; set; } = string.Empty;

    /// <summary>
    /// Modelo del vehículo.
    /// </summary>
    public string Modelo { get; set; } = string.Empty;

    /// <summary>
    /// Año de fabricación del vehículo.
    /// </summary>
    public int Año { get; set; }

    /// <summary>
    /// Precio del vehículo.
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// Stock disponible del vehículo.
    /// </summary>
    public int Stock { get; set; }
}
