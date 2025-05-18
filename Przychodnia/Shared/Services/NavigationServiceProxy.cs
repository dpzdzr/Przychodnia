using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Shared.Services;

public class NavigationServiceProxy : INavigationService
{
    public INavigationService Current { get; set; }
    public void NavigateBack()
        => Current?.NavigateBack();

    public void NavigateTo(BaseViewModel viewModel)
        => Current?.NavigateTo(viewModel);
}
