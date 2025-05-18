using Przychodnia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Models;

public class PostalCode : IEntity
{
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string City { get; set; }
}
