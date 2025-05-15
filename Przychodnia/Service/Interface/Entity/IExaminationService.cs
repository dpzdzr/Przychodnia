using Przychodnia.Model;
using Przychodnia.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Service.Interface.Entity;

public interface IExaminationService
{
    Task<Examination> AddAsync(ExaminationDTO dto);
    Task RemoveAsync(int id);
    Task<IEnumerable<Examination>> GetAllAsync();
    Task<IEnumerable<Examination>> GetAllWithDetailsAsync();
    Task UpdateAsync(int id,  ExaminationDTO dto);
}
