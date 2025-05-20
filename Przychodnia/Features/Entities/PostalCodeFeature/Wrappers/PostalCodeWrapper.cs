using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;

public partial class PostalCodeWrapper : BaseWrapper
{
    [ObservableProperty]
    //[NotifyDataErrorInfo]
    //[Required(ErrorMessage = "Kod pocztowy jest wymagany")]
    //[RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Kod pocztowy musi być w formacie\nxx-xxx")]
    private string? code;

    [ObservableProperty]
    //[NotifyDataErrorInfo]
    //[Required(ErrorMessage = "Miasto jest wymagane")]
    private string? city;

    public PostalCodeWrapper() { }
    public PostalCodeWrapper(PostalCode? entity, bool createDummy = false)
    {
        if (entity is null)
        {
            if (!createDummy)
                throw new ArgumentNullException(nameof(entity), "Kod pocztowy nie może być null, chyba że jawnie tworzysz obiekt dummy.");

            Id = null;
            Code = null;
            City = null;
        }
        else
        {
            Id = entity.Id;
            Code = entity.Code;
            City = entity.City;
        }
    }
}
