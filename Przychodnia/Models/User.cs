using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Models;

class User
{
    public int Id { get; set; }
    [Required]
    public int UserTypeId { get; set; }
    public virtual UserType UserType { get; set; }
    
    [Required]
    public string Login { get; set; }
    public string? PasswordHash { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? LicenseNumber { get; set; }

    public bool? IsActive { get; set; }

    public int? LaboratoryId { get; set; }
    public virtual Laboratory Laboratory { get; set; }

    [InverseProperty("AttendingDoctor")]
    public virtual ICollection<Appointment> AttendedAppointments { get; set; }
    [InverseProperty("ScheduledBy")]
    public virtual ICollection<Appointment> ScheduledAppointments { get; set; }
}
