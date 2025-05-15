namespace Przychodnia.Model;

public class UserType
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public enum UserTypeEnum
{
    Admin = 1,
    Lekarz,
    Laborant,
    Rejestrator,
    Menadżer,
    KierownikLaboratorium
}

