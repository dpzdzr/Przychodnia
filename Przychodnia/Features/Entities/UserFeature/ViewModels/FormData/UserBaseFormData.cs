using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.LaboratoryFeature.Wrappers;
using Przychodnia.Features.Entities.UserTypesFeature.Wrappers;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.UserFeature.ViewModels.FormData;

public abstract partial class UserBaseFormData : ObservableValidator
{

    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Login jest wymagany")]
    private string? login;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Hasło jest wymagane")]
    private string? passwordHash;

    [ObservableProperty] private string? licenseNumber;
    [ObservableProperty] private bool? isActive = false;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Rola jest wymagana")] 
    private UserTypeWrapper? selectedUserType;

    [ObservableProperty] private LaboratoryWrapper? selectedLaboratory;
    [ObservableProperty] private LaboratoryWrapper? managedLaboratory;

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
