using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;

namespace Przychodnia.ViewModel.Form;

public abstract partial class UserFormDataBase : ObservableValidator
{

    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;
    [ObservableProperty] private string? login;
    [ObservableProperty] private string? passwordHash;
    [ObservableProperty] private string? licenseNumber;
    [ObservableProperty] private bool? isActive = false;
    [ObservableProperty] private UserType? selectedUserType;
    [ObservableProperty] private Laboratory? selectedLaboratory;
    [ObservableProperty] private Laboratory? managedLaboratory;
}
