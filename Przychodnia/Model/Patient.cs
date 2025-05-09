using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Model;
public class Patient
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
    public PostalCode? PostalCode {  get; set; }
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
