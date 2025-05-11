using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Wrapper;

public partial class UserTypeWrapper : ObservableObject
{
    [ObservableProperty] private int id;
    [ObservableProperty] private string name;

    public UserTypeWrapper(UserType entity)
    {
        Id = entity.Id;
        Name = entity.Name;
    }

    public UserTypeEnum Type => (UserTypeEnum)Id;
    public bool IsDoctor
        => Type == UserTypeEnum.Lekarz;
    public bool IsLabTechnician
        => Type == UserTypeEnum.Laborant;
    public bool IsLabManager
        => Type == UserTypeEnum.KierownikLaboratorium;
    public bool HasLicenseNumber => IsDoctor || IsLabTechnician || IsLabManager;
}
