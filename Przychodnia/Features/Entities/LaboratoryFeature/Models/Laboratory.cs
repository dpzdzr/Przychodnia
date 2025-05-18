using Przychodnia.Core.Interfaces;
using Przychodnia.Features.Entities.UserFeature.Models;

namespace Przychodnia.Features.Entities.LaboratoryFeature.Models;
public class Laboratory : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int? ManagerId { get; set; }
    public virtual User? Manager { get; set; }
    public virtual ICollection<User>? Workers { get; set; }
}
