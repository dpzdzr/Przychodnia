using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.ViewModel.Form;

public partial class AppointmentEditFormData : AppointmentBaseFormData
{
    [ObservableProperty] private int id;
}
