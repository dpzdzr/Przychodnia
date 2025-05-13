using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Form;

public abstract partial class UserBaseFormData : ObservableValidator
{

    [ObservableProperty] private string? firstName;
    [ObservableProperty] private string? lastName;
    [ObservableProperty] private string? login;
    [ObservableProperty] private string? passwordHash;
    [ObservableProperty] private string? licenseNumber;
    [ObservableProperty] private bool? isActive = false;
    [ObservableProperty] private UserTypeWrapper? selectedUserType;
    [ObservableProperty] private LaboratoryWrapper? selectedLaboratory;
    [ObservableProperty] private LaboratoryWrapper? managedLaboratory;
}
