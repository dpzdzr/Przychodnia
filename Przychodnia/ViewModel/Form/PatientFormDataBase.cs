using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;

namespace Przychodnia.ViewModel.Form;

public abstract partial class PatientFormDataBase : ObservableObject
{
    [ObservableProperty] private string firstName;
    [ObservableProperty] private string lastName;
    [ObservableProperty] private string pesel;
    [ObservableProperty] private string street;
    [ObservableProperty] private string houseNumber;
    [ObservableProperty] private string apartmentNumber;
    [ObservableProperty] private PostalCode postalCode;
    [ObservableProperty] private Sex sex;
}
