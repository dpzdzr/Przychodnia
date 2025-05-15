using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.MappingProfile;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // user <-> dto
        CreateMap<UserDTO, User>();

        // wrapper <-> dto
        CreateMap<UserWrapper, UserDTO>();


        // wrapper <-> formData
        CreateMap<UserWrapper, UserEditFormData>()
            .ForMember(dest => dest.SelectedUserType, opt => opt.MapFrom(src => src.UserType))
            .ForMember(dest => dest.SelectedLaboratory, opt => opt.MapFrom(src => src.Laboratory));

        CreateMap<UserEditFormData, UserWrapper>()
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.SelectedUserType))
            .ForMember(dest => dest.Laboratory, opt => opt.MapFrom(src => src.SelectedLaboratory));

        // formData <-> dto
        CreateMap<UserAddFormData, UserDTO>()
            .ForMember(dest => dest.UserTypeId, opt => opt.MapFrom(src => src.SelectedUserType.Id))
            .ForMember(dest => dest.LaboratoryId, opt => opt.MapFrom(src => src.SelectedLaboratory.Id));
    }
}
