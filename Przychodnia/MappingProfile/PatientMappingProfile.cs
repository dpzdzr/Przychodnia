using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.MappingProfile;

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
