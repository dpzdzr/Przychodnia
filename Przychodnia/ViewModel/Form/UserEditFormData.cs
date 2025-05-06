using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.ViewModel.Form;

public partial class UserEditFormData : UserFormDataBase
{
    [ObservableProperty] private int id;
}
