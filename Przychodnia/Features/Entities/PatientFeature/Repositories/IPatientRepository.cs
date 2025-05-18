using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.PatientFeature.Models;

namespace Przychodnia.Features.Entities.PatientFeature.Repositories;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<IEnumerable<Patient>> GetAllWithDetailsAsync();
    Task<Patient?> GetByPesel(string pesel);
}
