using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class UserWrapper : ObservableObject
{
    public int Id { get; }

    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;
    [ObservableProperty] private string login;
    [ObservableProperty] private string? password;
    [ObservableProperty] private bool isActive;
    [ObservableProperty] private UserType? userType;
    [ObservableProperty] private Laboratory? laboratory;

    public string FullName => $"{FirstName} {LastName}".Trim();

    public UserWrapper(User user)
    {
        Id = user.Id;
        Login = user.Login;
        Password = user.PasswordHash;
        FirstName = user.FirstName;
        LastName = user.LastName;
        IsActive = (bool)user.IsActive;
        UserType = user.UserType;
        Laboratory = user.Laboratory;
    }

    public void UpdateFrom(User user)
    {
        Login = user.Login;
        Password = user.PasswordHash;
        FirstName = user.FirstName;
        LastName = user.LastName;
        IsActive = (bool)user.IsActive;
        UserType = user.UserType;
        Laboratory = user.Laboratory;
    }

    public User ToEntity() => new()
    {
        Id = Id,
        Login = Login,
        PasswordHash = Password,
        FirstName = FirstName,
        LastName = LastName,
        IsActive = IsActive,
        UserType = UserType,
        Laboratory = Laboratory
    };
}
