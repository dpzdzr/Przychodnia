using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Core.Repositories;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Repositories;

public interface IPostalCodeRepository : IBaseRepository<PostalCode>
{
}
