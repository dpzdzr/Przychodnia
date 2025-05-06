using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;

namespace Przychodnia.Service.Interface;

public interface IPatientService
{
    Task<List<Patient>> GetAllAsync();
}
