using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;

public abstract partial class PatientBaseFormData : ObservableValidator
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Imię jest wymagane")]
    private string? firstName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    private string? lastName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Pesel jest wymagany")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "PESEL musi zawierać dokładnie 11 cyfr")] 
    private string? pesel;

    [ObservableProperty] private string? street;
    [ObservableProperty] private string? houseNumber;
    [ObservableProperty] private string? apartmentNumber;
    [ObservableProperty] private PostalCodeWrapper? postalCode;
    [ObservableProperty] private Sex sex;

    public bool IsValid
    {
        get
        {
            ValidateAllProperties();
            return !HasErrors;
        }
    }

    public void ClearAllErrors()
    {
        var errorPropertyNames = GetErrors()
            .SelectMany(e => e.MemberNames)
            .Distinct()
            .ToList();

        foreach (var propertyName in errorPropertyNames)
        {
            ClearErrors(propertyName);
        }
    }

}
