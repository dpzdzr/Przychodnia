using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Form;

public abstract partial class AppointmentBaseFormData : ObservableObject
{
    [ObservableProperty] private DateTime? date;
    [ObservableProperty] private bool? completed;
    [ObservableProperty] private UserWrapper? scheduledBy;
    [ObservableProperty] private UserWrapper? attendingDoctor;
    [ObservableProperty] private PatientWrapper? patient;
}
