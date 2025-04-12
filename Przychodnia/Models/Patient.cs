using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Models;
class Patient
{
    public int Id { get; set; }
    [StringLength(11)]
    public string? Pesel { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string? Address {  get; set; }
    public Sex? sex { get; set; }
}

enum Sex
{
    Male,
    Female
}
