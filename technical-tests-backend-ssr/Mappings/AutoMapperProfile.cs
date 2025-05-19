using AutoMapper;
using technical_tests_backend_ssr.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<ClienteDTO, Cliente>().ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Vehiculo, VehiculoDTO>().ReverseMap();
        CreateMap<VehiculoDTO, Vehiculo>().ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Venta, VentaDTO>().ReverseMap();
        CreateMap<VentaDTO, Venta>().ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<ServicioPostVenta, ServicioPostVentaDTO>().ReverseMap();
        CreateMap<ServicioPostVentaDTO, ServicioPostVenta>().ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}

