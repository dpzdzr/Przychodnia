using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Model;

public class UserType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UserTypeEnum Type => (UserTypeEnum)Id;
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

