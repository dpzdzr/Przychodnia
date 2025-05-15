using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Wrapper;

public partial class PostalCodeWrapper : BaseWrapper
{
    [ObservableProperty] private string? code;
    [ObservableProperty] private string? city;

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
    // needed for mapping PatientWrapper -> EditForm
    public PostalCodeWrapper() { }
}
