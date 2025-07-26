using AutoMapper;
using PatientProject.Api.Models;
using PatientProject.Application.Models;
using PatientProject.Domain.Entites;
using PatientProject.Domain.Enums;

namespace PatientProject.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PatientRequest, PatientDto>();
        CreateMap<NameRequest, NameDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<PatientDto, Patient>()
        .ForMember(dest => dest.Gender,
            opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender, true)))
        .ForMember(dest => dest.Active,
            opt => opt.MapFrom(src => Enum.Parse<Active>(src.Active, true)));

        CreateMap<NameDto, Name>();
    }

}
