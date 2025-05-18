using Przychodnia.Core.Repositories;
using Przychodnia.Data;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;

namespace Przychodnia.Features.Entities.PostalCodeFeature.Repositories;

class PostalCodeRepository(AppDbContext context)
    : BaseRepository<PostalCode, AppDbContext>(context), IPostalCodeRepository
{
}
