using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.LaboratoryFeature.Wrappers;
using Przychodnia.Features.Entities.UserTypesFeature.Wrappers;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.Entities.UserFeature.ViewModels.FormData;

public abstract partial class UserBaseFormData : BaseFormData
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Login jest wymagany")]
    private string? login;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Hasło jest wymagane")]
    private string? passwordHash;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Rola jest wymagana")]
    private UserTypeWrapper? selectedUserType;

    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;
    [ObservableProperty] private string? licenseNumber;
    [ObservableProperty] private bool? isActive = false;
    [ObservableProperty] private LaboratoryWrapper? selectedLaboratory;
    [ObservableProperty] private LaboratoryWrapper? managedLaboratory;
}
