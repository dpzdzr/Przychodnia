using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;

namespace Przychodnia.ViewModel.Forms;

public partial class UserFormData : ObservableObject
{
    [ObservableProperty] private string firstName;
    [ObservableProperty] private string lastName;
    [ObservableProperty] private string login;
    [ObservableProperty] private string password;
    [ObservableProperty] private string licenseNumber;
    [ObservableProperty] private bool? isActive;
    [ObservableProperty] private UserType? selectedUserType;
    [ObservableProperty] private Laboratory? selectedLaboratory;
}
