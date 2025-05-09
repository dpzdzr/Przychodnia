using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class PatientWrapper(Patient entity) : ObservableObject
{
    [ObservableProperty] private int id = entity.Id;
    [ObservableProperty] private string pesel = entity.Pesel;
    [ObservableProperty] private string firstName = entity.FirstName;
    [ObservableProperty] private string lastName = entity.LastName;
    [ObservableProperty] private PostalCodeWrapper? postalCode = entity.PostalCode is null ? null : new(entity.PostalCode);
    [ObservableProperty] private string? street = entity.Street;
    [ObservableProperty] private string? houseNumber = entity.HouseNumber;
    [ObservableProperty] private string? apartmentNumber = entity.ApartmentNumber;
    [ObservableProperty] private Sex? sex = entity.Sex;
}
