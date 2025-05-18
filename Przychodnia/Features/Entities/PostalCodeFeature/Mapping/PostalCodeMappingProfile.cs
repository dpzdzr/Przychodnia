using AutoMapper;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
