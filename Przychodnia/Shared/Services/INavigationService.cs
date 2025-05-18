using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Shared.Services;

public interface INavigationService
{
    void NavigateTo(BaseViewModel viewModel);
    void NavigateBack();
}
