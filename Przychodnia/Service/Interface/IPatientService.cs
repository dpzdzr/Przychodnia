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
    Task RemoveAsync(Patient patient);
    Task AddAsync(PatientInputDto patientDTO);
}
