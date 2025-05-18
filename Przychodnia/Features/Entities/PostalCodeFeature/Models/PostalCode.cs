using Przychodnia.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Models;

public class PostalCode : IEntity
{
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string City { get; set; }
}
