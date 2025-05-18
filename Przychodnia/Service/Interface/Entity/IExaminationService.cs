using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface IExaminationService
{
    Task<Examination> AddAsync(ExaminationDTO dto);
    Task RemoveAsync(int id);
    Task<IEnumerable<Examination>> GetAllAsync();
    Task<IEnumerable<Examination>> GetAllWithDetailsAsync();
    Task<Examination?> CreateAsync(ExaminationDTO dto);
    Task UpdateAsync(int id, ExaminationDTO dto);
}
