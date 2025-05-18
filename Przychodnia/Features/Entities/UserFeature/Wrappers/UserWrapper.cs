using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Features.Entities.LaboratoryFeature.Wrappers;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Wrappers;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.Features.Entities.UserFeature.Wrappers;

public partial class UserWrapper : BaseWrapper
{
    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;
    [ObservableProperty] private string? login;
    [ObservableProperty] private string? passwordHash;
    [ObservableProperty] private string? licenseNumber;
    [ObservableProperty] private bool? isActive;
    [ObservableProperty] private UserTypeWrapper? userType;
    [ObservableProperty] private LaboratoryWrapper? laboratory;
    [ObservableProperty] private LaboratoryWrapper? managedLaboratory;

    public UserWrapper() { }
    public UserWrapper(User? user, bool includeLaboratories = true, bool createDummy = false)
    {
        if (user is null)
        {
            if (!createDummy)
                throw new ArgumentNullException(nameof(user), "Użytkownik nie może być null, chyba że jawnie tworzysz obiekt dummy.");

            Id = null;
            FirstName = "brak";
            LastName = string.Empty;
        }
        else
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Login = user.Login;
            PasswordHash = user.PasswordHash;
            LicenseNumber = user.LicenseNumber;
            IsActive = user.IsActive;
            UserType = WrapIfNotNull(user.UserType, l => new UserTypeWrapper(l));

            if (includeLaboratories)
            {
                Laboratory = WrapIfNotNull(user.Laboratory, l => new LaboratoryWrapper(l));
                ManagedLaboratory = WrapIfNotNull(user.ManagedLaboratory, ml => new LaboratoryWrapper(ml));
            }
        }
    }

    public string FullName => $"{FirstName} {LastName}".Trim();
}
