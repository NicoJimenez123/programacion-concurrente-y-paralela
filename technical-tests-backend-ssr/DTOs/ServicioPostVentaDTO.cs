/// <summary>
/// DTO para representar un servicio de postventa.
/// </summary>
public class ServicioPostVentaDTO
{
    /// <summary>
    /// Identificador único del servicio de postventa.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador del cliente que solicitó el servicio de postventa.
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Tipo de servicio solicitado.
    /// </summary>
    public string TipoServicio { get; set; } = string.Empty;

    /// <summary>
    /// Fecha en la que se solicitó el servicio de postventa.
    /// </summary>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Estado del servicio de postventa.
    /// </summary>
    public string Estado { get; set; } = string.Empty;

}