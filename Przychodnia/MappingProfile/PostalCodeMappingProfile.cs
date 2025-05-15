using AutoMapper;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.MappingProfile;

public class PostalCodeMappingProfile : Profile
{
    public PostalCodeMappingProfile()
    {
        CreateMap<PostalCodeDTO, PostalCode>();
        CreateMap<PostalCodeWrapper, PostalCodeWrapper>();
        CreateMap<PostalCodeWrapper, PostalCodeDTO>().ReverseMap();
    }
}
