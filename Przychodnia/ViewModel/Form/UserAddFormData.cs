using Przychodnia.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Form;

public class UserAddFormData : UserFormDataBase
{
    public void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Login = string.Empty;
        Password = string.Empty;
        LicenseNumber = string.Empty;
        IsActive = false;
        SelectedUserType = null;
        SelectedLaboratory = null;
    }

    public UserDTO ToDTO() => new()
    {
        FirstName = FirstName,
        LastName = LastName,
        Login = Login,
        PasswordHash = Password,
        LicenseNumber = LicenseNumber,
        IsActive = IsActive,
        UserType = SelectedUserType,
        Laboratory = SelectedLaboratory
    };
}
