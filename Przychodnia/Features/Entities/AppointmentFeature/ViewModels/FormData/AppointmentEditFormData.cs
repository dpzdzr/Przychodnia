using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;

public partial class AppointmentEditFormData : AppointmentBaseFormData
{
    [ObservableProperty] private int id;
}
