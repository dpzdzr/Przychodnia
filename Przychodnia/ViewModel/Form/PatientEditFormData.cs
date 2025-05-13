using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;

namespace Przychodnia.ViewModel.Form;

public partial class PatientEditFormData : PatientBaseFormData
{
    [ObservableProperty] private int id;
}
