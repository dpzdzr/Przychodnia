using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;

namespace Przychodnia.ViewModel.Form;

public partial class UserEditFormData : UserFormDataBase
{
    [ObservableProperty] private int id;

    public void LoadFromUser(User user)
    {
        this.Id = user.Id;
        this.FirstName = user.FirstName;
        this.LastName = user.LastName;
        this.Login = user.Login;
        this.Password = user.PasswordHash;
        this.LicenseNumber = user.LicenseNumber;
        this.SelectedLaboratory = user.Laboratory;
        this.SelectedUserType = user.UserType;
        this.IsActive = user.IsActive;
    }

    public void LoadToUser(User user)
    {
        user.FirstName = this.FirstName;
        user.LastName = this.LastName;
        user.Login = this.Login;
        user.PasswordHash = this.Password;
        user.LicenseNumber = this.LicenseNumber;
        user.Laboratory = this.SelectedLaboratory;
        user.UserType = this.SelectedUserType;
        user.IsActive = this.IsActive;
    }
}
