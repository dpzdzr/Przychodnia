using Przychodnia.Core.Interfaces;

namespace Przychodnia.Features.Entities.UserTypesFeature.Models;

public class UserType : IEntity
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

