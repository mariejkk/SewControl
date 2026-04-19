using AutoMapper;
using SewControl.Application.Dtos.Encargos;
using SewControl.Application.Dtos.Usuarios;
using SewControl.Domain.Entities.Encargos;
using SewControl.Domain.Entities.Usuarios;

namespace SewControl.Application.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
      
        CreateMap<Cliente, ClienteDto>()
            .ForMember(dest => dest.TotalEncargos, opt => opt.MapFrom(src => src.Encargos.Count));
        CreateMap<CreateClienteDto, Cliente>();

      
        CreateMap<Costurera, CostureraDto>()
            .ForMember(dest => dest.EncargosActivos,
                opt => opt.MapFrom(src => src.Encargos.Count(e => e.Estado != EstadoEncargo.Entregado && e.Estado != EstadoEncargo.Cancelado)));
        CreateMap<CreateCostureraDto, Costurera>();

      
        CreateMap<Encargo, EncargoDto>()
            .ForMember(dest => dest.NombreCliente,
                opt => opt.MapFrom(src => $"{src.Cliente.Nombre} {src.Cliente.Apellido}"))
            .ForMember(dest => dest.NombreCosturera,
                opt => opt.MapFrom(src => $"{src.Costurera.Nombre} {src.Costurera.Apellido}"));
        CreateMap<CreateEncargoDto, Encargo>();

      
        CreateMap<Prenda, PrendaDto>();
        CreateMap<CreatePrendaDto, Prenda>();

      
        CreateMap<Arreglo, ArregloDto>();
        CreateMap<CreateArregloDto, Arreglo>();
    }
}