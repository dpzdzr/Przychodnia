namespace Przychodnia.Features.Entities.UserFeature.Models;

public record UserDTO
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string? LicenseNumber { get; set; }
    public int UserTypeId { get; set; }
    public int? LaboratoryId { get; set; }
    public int? ManagedLaboratoryId { get; set; }
    public bool IsActive { get; set; }
}