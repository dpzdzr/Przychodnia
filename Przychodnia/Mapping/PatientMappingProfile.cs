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

namespace Przychodnia.Mapping;

public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<PatientWrapper, PatientDTO>().ReverseMap();
        CreateMap<PatientEditFormData, PatientDTO>().ReverseMap();
        CreateMap<PatientWrapper, PatientEditFormData>().ReverseMap();
        CreateMap<PatientAddFormData, PatientDTO>();
        CreateMap<Patient, PatientDTO>().ReverseMap();
    }
}
