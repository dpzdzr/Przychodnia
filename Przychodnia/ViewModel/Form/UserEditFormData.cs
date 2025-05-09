using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Form;

public partial class UserEditFormData : UserFormDataBase
{
    [ObservableProperty] private int id;

    public void LoadFromUserWrapper(UserWrapper user)
    {
        this.Id = user.Id;
        this.FirstName = user.FirstName;
        this.LastName = user.LastName;
        this.Login = user.Login;
        this.PasswordHash = user.PasswordHash;
        this.LicenseNumber = user.LicenseNumber;
        this.SelectedLaboratory = user.Laboratory;
        this.SelectedUserType = user.UserType;
        this.IsActive = user.IsActive;
    }
}
