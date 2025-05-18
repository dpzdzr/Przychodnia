using Przychodnia.Core.Interfaces;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Models;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.ExaminationFeature.Models;

public class Examination : IEntity
{
    public int Id { get; set; }
    [Required]
    public ExaminationType ExaminationType { get; set; }

    [Required]
    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    [Required]
    public int OrderedById { get; set; }
    public User OrderedBy { get; set; }

    public int PerformingDoctorId { get; set; }
    public User PerformingDoctor { get; set; }

    public int PerformingLaboratoryId { get; set; }
    public Laboratory PerformingLaboratory { get; set; }
}

public enum ExaminationType
{
    Physical,
    Laboratory
}

