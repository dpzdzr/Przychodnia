using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.PatientFeature.Models;

namespace Przychodnia.Features.Entities.PatientFeature.Services;

public interface IPatientService : IBaseEntityService<Patient, PatientDTO>
{
    Task<IEnumerable<Patient>> GetAllWithDetailsAsync();
    Task<Patient?> GetByPeselAsync(string pesel);
}
