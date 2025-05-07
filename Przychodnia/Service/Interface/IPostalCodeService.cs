using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface;

public interface IPostalCodeService
{
    Task CreateAsync(PostalCodeInputDTO code);
    Task<List<PostalCode>> GetAllAsync();
    Task SaveChangesAsync();

    Task RemoveAsync(PostalCode code);

    Task<List<PostalCode>> Filter(string fragment);
}
