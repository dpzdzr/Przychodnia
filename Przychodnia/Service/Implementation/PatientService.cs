using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Interface;

namespace Przychodnia.Service.Implementation;

public class PatientService(IPatientRepository repo) : IPatientService
{   
    private readonly IPatientRepository _repo = repo;
    public async Task<List<Patient>> GetAllAsync()
        => await _repo.GetAllAsync();
}
