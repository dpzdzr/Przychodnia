using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Base;


namespace Przychodnia.ViewModel.Wrapper;

public partial class AppointmentWrapper : BaseWrapper
{
    [NotifyPropertyChangedFor(nameof(OnlyDate))]
    [NotifyPropertyChangedFor(nameof(OnlyTime))]
    [ObservableProperty]
    private DateTime? date;
    [ObservableProperty] private bool? completed;
    [ObservableProperty] private UserWrapper? scheduledBy;
    [ObservableProperty] private UserWrapper? attendingDoctor;
    [ObservableProperty] private PatientWrapper? patient;
    public AppointmentWrapper(Appointment entity)
    {
        Id = entity.Id;
        Date = entity.Date;
        Completed = entity.Completed;
        ScheduledBy = WrapIfNotNull(entity.ScheduledBy, s => new UserWrapper(s, false));
        AttendingDoctor = WrapIfNotNull(entity.AttendingDoctor, s => new UserWrapper(s, false));
        Patient = WrapIfNotNull(entity.Patient, p => new PatientWrapper(p));
    }

    public DateOnly? OnlyDate => Date.HasValue ? DateOnly.FromDateTime(Date.Value) : null;
    public TimeOnly? OnlyTime => Date.HasValue ? TimeOnly.FromDateTime(Date.Value) : null;
}
