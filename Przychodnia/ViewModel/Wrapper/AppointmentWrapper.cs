using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using static Przychodnia.ViewModel.Wrapper.WrapperHelper;

namespace Przychodnia.ViewModel.Wrapper;

public partial class AppointmentWrapper : ObservableObject
{
    [ObservableProperty] private int? id;
    [ObservableProperty] private DateTime? date;
    [ObservableProperty] private bool? completed;
    [ObservableProperty] private UserWrapper? scheduledBy;
    [ObservableProperty] private UserWrapper? attendingDoctor;
    [ObservableProperty] private PatientWrapper? patient;

    public AppointmentWrapper(Appointment entity)
    {
        Id = entity.Id;
        Date = entity.Date;
        Completed = entity.Completed;
        ScheduledBy = WrapPropertyIfNotNull(entity.ScheduledBy, s => new UserWrapper(s, false));
        AttendingDoctor = WrapPropertyIfNotNull(entity.AttendingDoctor, s => new UserWrapper(s, false));
        Patient = WrapPropertyIfNotNull(entity.Patient, p => new PatientWrapper(p));

    }
}
