public class FacturaVentaDTO
{
    public int VentaId { get; set; }
    public string ClienteNombre { get; set; }
    public VehiculoCaracteristicasDTO Vehiculo { get; set; }
    public decimal Total { get; set; }
    public DateTime Fecha { get; set; }
}