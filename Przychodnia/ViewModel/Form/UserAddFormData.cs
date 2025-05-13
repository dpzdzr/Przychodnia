using Przychodnia.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Form;

public class UserAddFormData : UserBaseFormData
{
    public void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Login = string.Empty;
        PasswordHash = string.Empty;
        LicenseNumber = string.Empty;
        IsActive = false;
        SelectedUserType = null;
        SelectedLaboratory = null;
        ManagedLaboratory = null;
    }
}
