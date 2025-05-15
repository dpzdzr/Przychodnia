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
    [ObservableProperty] private TimeSpan? selectedHour;
    [ObservableProperty] private DateTime? selectedDate;
    [ObservableProperty] private UserWrapper? scheduledBy;
    [ObservableProperty] private UserWrapper? selectedDoctor;
    [ObservableProperty] private string enteredPatientPesel = string.Empty;
    [ObservableProperty] private bool? completed;

    public PatientWrapper? SelectedPatient { get; set; }
    public DateTime? FullDate =>
       SelectedDate is not null && SelectedHour is not null ? SelectedDate.Value.Date + SelectedHour.Value : null;
}
