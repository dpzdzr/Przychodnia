using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Repositories;

public interface ILaboratoryRepository : IBaseRepository<Laboratory>
{
    Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync();

}
