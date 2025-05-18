namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels.FormData;

public class AppointmentAddFormData : AppointmentBaseFormData
{
    public void ClearForm()
    {
        SelectedDate = null;
        SelectedHour = null;
        Completed = null;
        ScheduledBy = null;
        SelectedDoctor = null;
        SelectedPatient = null;
        EnteredPatientPesel = string.Empty;

        ClearAllErrors();
    }
}
