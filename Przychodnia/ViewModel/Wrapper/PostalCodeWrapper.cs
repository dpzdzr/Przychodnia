using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class PostalCodeWrapper : BaseWrapper
{
    [ObservableProperty] private string? code;
    [ObservableProperty] private string? city;

    public PostalCodeWrapper(PostalCode entity)
    {
        Id = entity.Id;
        Code = entity.Code;
        City = entity.City;
    }
    // needed for mapping PatientWrapper -> EditForm
    public PostalCodeWrapper() { } 
}
