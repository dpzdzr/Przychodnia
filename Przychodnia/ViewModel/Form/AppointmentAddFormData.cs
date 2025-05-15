namespace Przychodnia.ViewModel.Form;

public class AppointmentAddFormData : AppointmentBaseFormData
{
    public void ClearForm()
    {
        Date = null;
        Completed = null;
        ScheduledBy = null;
        AttendingDoctor = null;
        Patient = null;
    }

}
