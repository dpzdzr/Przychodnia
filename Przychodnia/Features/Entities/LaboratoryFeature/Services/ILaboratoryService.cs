using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Services;

public interface ILaboratoryService : IBaseService<Laboratory, LaboratoryDTO>
{
    Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync();
}
