using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Service.Interface;

namespace Przychodnia.Model.DTO;

public record UserInputDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Login { get; set; }
    public string? PasswordHash { get; set; }
    public string? LicenseNumber { get; set; }
    public UserType? UserType { get; set; }
    public Laboratory? Laboratory { get; set; }
    public bool? IsActive { get; set; }
}