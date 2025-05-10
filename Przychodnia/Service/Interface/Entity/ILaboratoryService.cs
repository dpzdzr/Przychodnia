using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface ILaboratoryService
{
    Task<Laboratory> AddAsync(LaboratoryDTO dto);
    Task RemoveAsync(int id);
    Task<IEnumerable<Laboratory>> GetAllAsync();
    Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync();

}
