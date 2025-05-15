using CommunityToolkit.Mvvm.ComponentModel;

namespace Przychodnia.ViewModel.Form;

public partial class UserEditFormData : UserBaseFormData
{
    [ObservableProperty] private int id;
}
