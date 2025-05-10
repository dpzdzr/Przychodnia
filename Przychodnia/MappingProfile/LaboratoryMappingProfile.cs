using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.MappingProfile;

public class LaboratoryMappingProfile : Profile
{
    public LaboratoryMappingProfile( )
    {
        CreateMap<LaboratoryDTO, Laboratory>();
        CreateMap<LaboratoryWrapper, LaboratoryWrapper>();
        CreateMap<LaboratoryWrapper, LaboratoryDTO>()
            .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.Manager.Id));
    }

}
