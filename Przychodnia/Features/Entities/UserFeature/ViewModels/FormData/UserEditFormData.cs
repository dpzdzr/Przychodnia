using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.Features.Entities.UserFeature.ViewModels.FormData;

public partial class UserEditFormData : UserBaseFormData
{
    [ObservableProperty] private int id;
}
