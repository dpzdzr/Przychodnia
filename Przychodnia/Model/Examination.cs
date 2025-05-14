using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Model;

public class Examination
{
    public int? Id { get; set; }
    //[Required]
    public ExaminationType? ExaminationType { get; set; }

    //[Required]
    public int? PatientId { get; set; }
    public Patient? Patient { get; set; }

    //[Required]
    public int? OrderedById { get; set; }
    public User? OrderedBy { get; set; }

    public int? PerformingDoctorId { get; set; }
    public User? PerformingDoctor { get; set; }

    public int? PerformingLaboratoryId { get; set; }
    public Laboratory? PerformingLaboratory { get; set; }
}

public enum ExaminationType
{
    Physical,
    Laboratory
}

