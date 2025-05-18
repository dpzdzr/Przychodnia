using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Services;

public interface ILaboratoryService : IBaseService<Laboratory, LaboratoryDTO>
{
    Task<IEnumerable<Laboratory>> GetAllWithDetailsAsync();
}
