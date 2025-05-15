namespace Przychodnia.Model.DTO;

public record PatientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pesel { get; set; }
    public int? PostalCodeId { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? ApartmentNumber { get; set; }
    public Sex? Sex { get; set; }
}
