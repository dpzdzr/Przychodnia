using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface IPostalCodeService
{
    Task<PostalCode> CreateAsync(PostalCodeInputDTO code);
    Task<List<PostalCode>> GetAllAsync();

    Task RemoveAsync(PostalCode code);

    Task<List<PostalCode>> GetAllMatchingByCode(string fragment);

    Task<List<PostalCode>> GetDistinctCodes(string fragment);

    Task UpdateAsync(PostalCode entity);

}
