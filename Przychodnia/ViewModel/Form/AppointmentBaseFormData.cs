using CommunityToolkit.Mvvm.ComponentModel;
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
