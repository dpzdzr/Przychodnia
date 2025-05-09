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

public class PostalCodeMappingProfile : Profile
{
    public PostalCodeMappingProfile()
    {
        CreateMap<PostalCodeWrapper, PostalCodeDTO>().ReverseMap();
    }
}
