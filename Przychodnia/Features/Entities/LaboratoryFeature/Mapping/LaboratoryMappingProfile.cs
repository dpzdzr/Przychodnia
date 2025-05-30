﻿using AutoMapper;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;
using Przychodnia.Features.Entities.LaboratoryFeature.Wrappers;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Mapping;

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
