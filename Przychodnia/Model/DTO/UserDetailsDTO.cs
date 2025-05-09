using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Model.DTO;

public record UserDetailsDTO
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string? LicenseNumber { get; set; }
    public bool IsActive { get; set; }
    public UserType UserType { get; set; }
    public Laboratory? Laboratory { get; set; }
}
