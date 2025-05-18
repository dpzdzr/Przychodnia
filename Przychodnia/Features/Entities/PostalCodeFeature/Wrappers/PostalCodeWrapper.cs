using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;

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
