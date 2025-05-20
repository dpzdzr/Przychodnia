using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Wrappers;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;

public abstract partial class PatientBaseFormData : BaseFormData
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
    
    [ObservableProperty] 
    private string? street;
    
    [ObservableProperty] 
    private string? houseNumber;
    
    [ObservableProperty] 
    private string? apartmentNumber;
    
    [ObservableProperty] 
    private PostalCodeWrapper? postalCode;
    
    [ObservableProperty] 
    private Sex sex;
}
