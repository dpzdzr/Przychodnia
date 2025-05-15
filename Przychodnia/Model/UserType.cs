using Przychodnia.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Model;

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

