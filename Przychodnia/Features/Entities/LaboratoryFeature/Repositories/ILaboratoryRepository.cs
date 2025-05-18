using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Repositories;

public interface ILaboratoryRepository : IBaseRepository<Laboratory>
{
    Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync();

}
