using AutoMapper;
using technical_tests_backend_ssr.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Cliente, ClienteDTO>();
        CreateMap<ClienteDTO, Cliente>();
        CreateMap<Vehiculo, VehiculoDTO>();
        CreateMap<VehiculoDTO, Vehiculo>();
        CreateMap<Venta, VentaDTO>();
        CreateMap<VentaDTO, Venta>();
        CreateMap<ServicioPostVenta, ServicioPostVentaDTO>();
        CreateMap<ServicioPostVentaDTO, ServicioPostVenta>();
    }
}

