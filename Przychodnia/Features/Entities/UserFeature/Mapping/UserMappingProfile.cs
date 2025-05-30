﻿using AutoMapper;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserFeature.ViewModels.FormData;
using Przychodnia.Features.Entities.UserFeature.Wrappers;

namespace Przychodnia.Features.Entities.UserFeature.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // user <-> dto
        CreateMap<UserDTO, User>().ReverseMap();

        // wrapper <-> dto
        CreateMap<UserWrapper, UserDTO>();

        CreateMap<User, UserWrapper>();


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
