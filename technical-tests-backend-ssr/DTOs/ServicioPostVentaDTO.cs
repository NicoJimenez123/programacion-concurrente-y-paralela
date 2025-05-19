// <summary>
// DTO para representar un servicio de postventa.
// </summary>
public class ServicioPostVentaDTO
{
    /// <summary>
    /// Identificador único del servicio de postventa.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descripción del servicio de postventa.
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Fecha en la que se solicitó el servicio de postventa.
    /// </summary>
    public DateTime FechaSolicitud { get; set; }

    /// <summary>
    /// Estado del servicio de postventa.
    /// </summary>
    public string Estado { get; set; } = string.Empty;
}