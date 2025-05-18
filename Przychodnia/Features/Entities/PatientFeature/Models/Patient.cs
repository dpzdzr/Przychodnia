using Przychodnia.Core.Interfaces;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.PatientFeature.Models;
public class Patient : IEntity
{

    public int Id { get; set; }
    [Required]
    [StringLength(11)]
    public string Pesel { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public int? PostalCodeId { get; set; }
    public PostalCode? PostalCode { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? ApartmentNumber { get; set; }
    public Sex? Sex { get; set; }
}

public enum Sex
{
    Male,
    Female
}
