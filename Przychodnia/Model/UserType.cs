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

    public bool IsDoctor => this.Type == UserTypeEnum.Lekarz;
    public bool IsLabTechnician => this.Type == UserTypeEnum.KierownikLaboratorium || this.Type == UserTypeEnum.Laborant;
    public bool IsDoctorOrLabTechnician => IsDoctor || IsLabTechnician;
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

