using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Core.Repositories;
using Przychodnia.Data;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Repositories;

class PostalCodeRepository(AppDbContext context)
    : BaseRepository<PostalCode, AppDbContext>(context), IPostalCodeRepository
{
}
