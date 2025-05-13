using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
