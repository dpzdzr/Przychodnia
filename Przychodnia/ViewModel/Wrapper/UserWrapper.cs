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

public partial class UserWrapper(User user) : ObservableObject
{
    [ObservableProperty] private int id = user.Id;
    [ObservableProperty] private string? firstName = user.FirstName;
    [ObservableProperty] private string? lastName = user.LastName;
    [ObservableProperty] private string? login = user.Login;
    [ObservableProperty] private string? passwordHash = user.PasswordHash;
    [ObservableProperty] private string? licenseNumber = user.LicenseNumber;
    [ObservableProperty] private bool isActive = user.IsActive;
    [ObservableProperty] private UserType userType = user.UserType;
    [ObservableProperty] private Laboratory? laboratory = user.Laboratory;
    public string FullName => $"{FirstName} {LastName}".Trim();
}
