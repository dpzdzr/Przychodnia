using Przychodnia.Data;
using Przychodnia.Model;
using Przychodnia.Repository.Interface;

namespace Przychodnia.Repository.Implementation;

class PostalCodeRepository(AppDbContext context)
    : BaseRepository<PostalCode, AppDbContext>(context), IPostalCodeRepository
{
}
