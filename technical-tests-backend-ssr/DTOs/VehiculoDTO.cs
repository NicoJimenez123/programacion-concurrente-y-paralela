/// <summary>
/// DTO para representar un vehículo.
/// </summary>
public class VehiculoDTO
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
    public int Anio { get; set; }

    /// <summary>
    /// Color del vehículo.
    /// </summary>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Número de identificación del vehículo (VIN).
    /// </summary>
    public string VIN { get; set; } = string.Empty;
}