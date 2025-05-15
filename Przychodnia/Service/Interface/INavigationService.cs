using Przychodnia.ViewModel.Base;

namespace Przychodnia.Service.Interface;

public interface INavigationService
{
    void NavigateTo(BaseViewModel viewModel);
    void NavigateBack();
}
