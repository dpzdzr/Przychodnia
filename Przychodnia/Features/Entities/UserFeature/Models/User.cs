using Przychodnia.Core.Interfaces;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Przychodnia.Features.Entities.UserFeature.Models;

public class User : IEntity
{
    public int Id { get; set; }
    [Required]
    public int UserTypeId { get; set; }
    public virtual UserType UserType { get; set; }

    [Required]
    public string Login { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? LicenseNumber { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public int? LaboratoryId { get; set; }
    public virtual Laboratory Laboratory { get; set; }
    public virtual Laboratory ManagedLaboratory { get; set; }

    [InverseProperty("AttendingDoctor")]
    public virtual ICollection<Appointment>? AttendedAppointments { get; set; } = [];
    [InverseProperty("ScheduledBy")]
    public virtual ICollection<Appointment>? ScheduledAppointments { get; set; } = [];
}
