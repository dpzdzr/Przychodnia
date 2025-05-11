using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Przychodnia.ViewModel.Wrapper.WrapperHelper;

namespace Przychodnia.ViewModel.Wrapper;

public partial class UserWrapper : ObservableObject
{
    [ObservableProperty] private int? id;
    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;
    [ObservableProperty] private string? login;
    [ObservableProperty] private string? passwordHash;
    [ObservableProperty] private string? licenseNumber;
    [ObservableProperty] private bool? isActive;
    [ObservableProperty] private UserTypeWrapper? userType;
    [ObservableProperty] private LaboratoryWrapper? laboratory;
    [ObservableProperty] private LaboratoryWrapper? managedLaboratory;

    public UserWrapper(User user, bool includeLaboratories = true)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Login = user.Login;
        PasswordHash = user.PasswordHash;
        LicenseNumber = user.LicenseNumber;
        IsActive = user.IsActive;
        UserType = WrapPropertyIfNotNull(user.UserType, l => new UserTypeWrapper(l));

        if (includeLaboratories)
        {
            Laboratory = 
                WrapPropertyIfNotNull(user.Laboratory, l => new LaboratoryWrapper(l));
            ManagedLaboratory = WrapPropertyIfNotNull(user.ManagedLaboratory, ml => new LaboratoryWrapper(ml));
        }
    }

    public string FullName => $"{FirstName} {LastName}".Trim();
}
