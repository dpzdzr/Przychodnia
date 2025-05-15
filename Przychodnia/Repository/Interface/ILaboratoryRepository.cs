using Przychodnia.Model;

namespace Przychodnia.Repository.Interface;

public interface ILaboratoryRepository : IBaseRepository<Laboratory>
{
    Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync();

}
