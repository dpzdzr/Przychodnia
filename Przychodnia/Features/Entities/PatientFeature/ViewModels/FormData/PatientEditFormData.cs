using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;

public partial class PatientEditFormData : PatientBaseFormData
{
    [ObservableProperty] private int id;
}
