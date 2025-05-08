using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface;

public interface IPatientService
{
    Task<List<Patient>> GetAllAsync();
    Task<IEnumerable<Patient>> GetAllWithDetailsAsync();
    Task RemoveAsync(Patient patient);
    Task AddAsync(PatientInputDto patientDTO);

    Task<Patient?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}
