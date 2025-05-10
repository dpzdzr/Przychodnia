using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;

namespace Przychodnia.Repository.Interface;

public interface ILaboratoryRepository : IBaseRepository<Laboratory>
{
    Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync();

}
