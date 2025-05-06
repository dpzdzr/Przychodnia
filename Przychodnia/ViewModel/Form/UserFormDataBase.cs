using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;

namespace Przychodnia.ViewModel.Form;

public abstract partial class UserFormDataBase : ObservableObject
{
    [ObservableProperty] protected string firstName;
    [ObservableProperty] protected string lastName;
    [ObservableProperty] protected string login;
    [ObservableProperty] protected string password;
    [ObservableProperty] protected string licenseNumber;
    [ObservableProperty] protected bool? isActive = false;
    [ObservableProperty] protected UserType? selectedUserType;
    [ObservableProperty] protected Laboratory? selectedLaboratory;
}
