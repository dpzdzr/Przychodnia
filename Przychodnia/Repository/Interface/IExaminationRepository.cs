using Przychodnia.Model;

namespace Przychodnia.Repository.Interface;

public interface IExaminationRepository : IBaseRepository<Examination>
{
    Task<IEnumerable<Examination>> GetAllWithDetailsAsync();
}
