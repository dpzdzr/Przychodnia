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
    [ObservableProperty] private int id;
    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;
    [ObservableProperty] private string? login;
    [ObservableProperty] private string? passwordHash;
    [ObservableProperty] private string? licenseNumber;
    [ObservableProperty] private bool? isActive;
    [ObservableProperty] private UserType userType;
    [ObservableProperty] private Laboratory? laboratory;

    public string FullName => $"{FirstName} {LastName}".Trim();

    public UserWrapper(User user)
    {
        Id = user.Id;
        Login = user.Login;
        PasswordHash = user.PasswordHash;
        FirstName = user.FirstName;
        LastName = user.LastName;
        LicenseNumber = user.LicenseNumber;
        IsActive = user.IsActive;
        UserType = user.UserType;
        Laboratory = user.Laboratory;
    }

    public void LoadFromForm(UserEditFormData form)
    {
        Login = form.Login;
        PasswordHash = form.PasswordHash;
        FirstName = form.FirstName;
        LastName = form.LastName;
        LicenseNumber = form.LicenseNumber;
        IsActive = form.IsActive;
        UserType = form.SelectedUserType;
        Laboratory = form.SelectedLaboratory;
    }

    //public UserDTO ToDTO() => new()
    //{
    //    FirstName = FirstName,
    //    LastName = LastName,
    //    Login = Login,
    //    PasswordHash = Password,
    //    LicenseNumber = LicenseNumber,
    //    IsActive = IsActive,
    //    UserType = UserType,
    //    Laboratory = Laboratory
    //};
}
