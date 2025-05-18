using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Core.Services;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Services;

public interface IPostalCodeService : IBaseService<PostalCode, PostalCodeDTO>
{
}
