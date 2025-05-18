using AutoMapper;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Mapping;

public class PostalCodeMappingProfile : Profile
{
    public PostalCodeMappingProfile()
    {
        CreateMap<PostalCodeDTO, PostalCode>();
        CreateMap<PostalCodeWrapper, PostalCodeWrapper>();
        CreateMap<PostalCodeWrapper, PostalCodeDTO>().ReverseMap();
    }
}
