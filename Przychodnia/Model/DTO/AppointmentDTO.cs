using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
