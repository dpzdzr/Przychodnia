using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Shared.Services.NavigationService;

public interface INavigationService
{
    void NavigateTo(BaseViewModel viewModel);
    void NavigateBack();
}
