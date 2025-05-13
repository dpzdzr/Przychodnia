using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class PatientWrapper : BaseWrapper
{
    [ObservableProperty] private string pesel;
    [ObservableProperty] private string firstName;
    [ObservableProperty] private string lastName;
    [ObservableProperty] private PostalCodeWrapper? postalCode;
    [ObservableProperty] private string? street;
    [ObservableProperty] private string? houseNumber;
    [ObservableProperty] private string? apartmentNumber;
    [ObservableProperty] private Sex? sex;

    public PatientWrapper(Patient entity)
    {
        Id = entity.Id;
        Pesel = entity.Pesel;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        PostalCode = WrapIfNotNull(entity.PostalCode, pc => new PostalCodeWrapper(pc));
        Street = entity.Street;
        HouseNumber = entity.HouseNumber;
        ApartmentNumber = entity.ApartmentNumber;
        Sex = entity.Sex;
    }
}

