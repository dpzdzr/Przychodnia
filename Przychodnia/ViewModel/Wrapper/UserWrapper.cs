using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.Model.DTO;
using Przychodnia.ViewModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    [ObservableProperty] private UserType? userType;
    [ObservableProperty] private Laboratory? laboratory;
    [ObservableProperty] private Laboratory? managedLaboratory;

    public UserWrapper(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Login = user.Login;
        PasswordHash = user.PasswordHash;
        LicenseNumber = user.LicenseNumber;
        IsActive = user.IsActive;
        UserType = user.UserType;
        Laboratory = user.Laboratory;
        ManagedLaboratory = user.ManagedLaboratory;
    }

    public string FullName => $"{FirstName} {LastName}".Trim();
}
