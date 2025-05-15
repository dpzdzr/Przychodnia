using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.ViewModel.Form;

public partial class PatientEditFormData : PatientBaseFormData
{
    [ObservableProperty] private int id;
}
