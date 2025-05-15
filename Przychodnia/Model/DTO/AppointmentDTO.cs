namespace Przychodnia.Model.DTO;

public record AppointmentDTO
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public bool? Completed { get; set; }
    public int? ScheduledById { get; set; }
    public int? AttendingDoctorId { get; set; }
    public int? PatientId { get; set; }
}
