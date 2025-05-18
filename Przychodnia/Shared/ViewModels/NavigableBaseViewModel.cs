using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Shared.Services.DialogService;

namespace Przychodnia.Shared.ViewModels;

public abstract partial class BaseNavigableViewModel(IDialogService dialogService)
    : BaseViewModel(dialogService)
{
    private readonly Stack<BaseViewModel> _navigationStack = new();

    [ObservableProperty] private BaseViewModel? currentViewModel;
    [ObservableProperty] private bool canNavigateBack;
    [ObservableProperty] private bool isBusy;

    protected void NavigateTo(BaseViewModel viewModel)
    {
        if (CurrentViewModel != null)
            _navigationStack.Push(CurrentViewModel);

        CurrentViewModel = viewModel;
        UpdateNavigationState();
    }
    protected void NavigateBack()
    {
        if (_navigationStack.Count > 0)
            CurrentViewModel = _navigationStack.Pop();
        UpdateNavigationState();
    }
    private void UpdateNavigationState()
    {
        CanNavigateBack = _navigationStack.Count > 0;
    }
}
