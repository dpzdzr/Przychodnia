namespace Przychodnia.Model;
public class Laboratory
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int? ManagerId { get; set; }
    public virtual User? Manager { get; set; }
    public virtual ICollection<User>? Workers { get; set; }
}
