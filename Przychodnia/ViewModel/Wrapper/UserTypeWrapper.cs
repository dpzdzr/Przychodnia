using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Model;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Wrapper;

public partial class UserTypeWrapper : BaseWrapper
{
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
