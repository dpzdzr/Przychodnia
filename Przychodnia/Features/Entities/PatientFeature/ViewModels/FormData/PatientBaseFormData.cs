using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;

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
