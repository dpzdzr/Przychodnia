namespace Przychodnia.Features.Entities.PatientFeature.ViewModels.FormData;

public class PatientAddFormData : PatientBaseFormData
{
    public void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Pesel = string.Empty;
        Street = string.Empty;
        HouseNumber = string.Empty;
        ApartmentNumber = string.Empty;
        PostalCode = null;
        Sex = default;
    }
}
