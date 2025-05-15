using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Model;

public class PostalCode
{
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string City { get; set; }
}
