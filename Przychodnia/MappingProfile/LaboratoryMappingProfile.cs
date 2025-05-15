using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.MappingProfile;

public class LaboratoryMappingProfile : Profile
{
    public LaboratoryMappingProfile()
    {
        CreateMap<LaboratoryDTO, Laboratory>();
        CreateMap<LaboratoryWrapper, LaboratoryWrapper>();
        CreateMap<LaboratoryWrapper, LaboratoryDTO>()
            .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.Manager.Id));
    }

}
