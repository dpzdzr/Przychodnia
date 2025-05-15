using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Form;

public abstract partial class PatientBaseFormData : ObservableObject
{
    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;
    [ObservableProperty] private string? pesel;
    [ObservableProperty] private string? street;
    [ObservableProperty] private string? houseNumber;
    [ObservableProperty] private string? apartmentNumber;
    [ObservableProperty] private PostalCodeWrapper? postalCode;
    [ObservableProperty] private Sex sex;
}
