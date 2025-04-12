using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Models;
class Appointment
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public bool? Completed { get; set; }

    [Required]
    public int ScheduledById { get; set; }
    public virtual User ScheduledBy { get; set; }

    [Required]
    public int AttendingDoctorId { get; set; }
    public virtual User AttendingDoctor { get; set; }

    [Required]
    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; }
}
