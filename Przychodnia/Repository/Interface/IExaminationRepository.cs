using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Repository.Interface;

public interface IExaminationRepository : IBaseRepository<Examination>
{
    Task<IEnumerable<Examination>> GetAllWithDetailsAsync();
}
