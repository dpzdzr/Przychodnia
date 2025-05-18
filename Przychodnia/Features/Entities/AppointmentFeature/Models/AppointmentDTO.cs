namespace Przychodnia.Features.Entities.AppointmentFeature.Models;

public record AppointmentDTO
{
    public DateTime Date { get; set; }
    public bool Completed { get; set; }
    public int? ScheduledById { get; set; }
    public int AttendingDoctorId { get; set; }
    public int PatientId { get; set; }
}
