using AutoMapper;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.PatientFeature.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.PatientFeature.Mapping;

public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<PatientWrapper, PatientDTO>()
            .ForMember(dest => dest.PostalCodeId, opt => opt.MapFrom(src => src.PostalCode.Id));
        CreateMap<PatientWrapper, PatientEditFormData>().ReverseMap();
        CreateMap<PatientAddFormData, PatientDTO>();
        CreateMap<PatientDTO, Patient>();
    }
}
