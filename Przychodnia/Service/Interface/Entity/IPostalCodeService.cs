using Przychodnia.Model;
using Przychodnia.Model.DTO;

namespace Przychodnia.Service.Interface.Entity;

public interface IPostalCodeService
{
    Task<PostalCode> CreateAsync(PostalCodeDTO code);
    Task<List<PostalCode>> GetAllAsync();
    Task RemoveAsync(int id);
    Task UpdateAsync(int id, PostalCodeDTO dto);
}
