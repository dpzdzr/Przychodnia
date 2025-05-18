using Przychodnia.Core.Interfaces;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Models;

namespace Przychodnia.Features.Entities.AppointmentFeature.Models;
public class Appointment : IEntity
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public bool? Completed { get; set; }

    //[Required]
    public int? ScheduledById { get; set; }
    public virtual User? ScheduledBy { get; set; }

    //[Required]
    public int? AttendingDoctorId { get; set; }
    public virtual User? AttendingDoctor { get; set; }

    //[Required]
    public int? PatientId { get; set; }
    public virtual Patient? Patient { get; set; }
}
