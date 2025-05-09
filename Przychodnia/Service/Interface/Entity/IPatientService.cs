using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface IPatientService
{
    Task<List<Patient>> GetAllAsync();
    Task<IEnumerable<Patient>> GetAllWithDetailsAsync();
    Task RemoveAsync(int id);
    Task <PatientDTO> CreateAsync(PatientDTO dto);
    Task UpdateAsync(int id, PatientDTO dto);
    Task<Patient?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}
