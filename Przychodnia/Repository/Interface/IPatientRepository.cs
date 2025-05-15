using Przychodnia.Model;

namespace Przychodnia.Repository.Interface;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<IEnumerable<Patient>> GetAllWithDetailsAsync();

}
