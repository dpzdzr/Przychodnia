using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Navigation;

public abstract class NavigableBaseViewModel : BaseViewModel
{
    private readonly Stack<BaseViewModel> _navigationStack = new();
    private BaseViewModel? _currentViewModel;
    private bool _canNavigateBack;
    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }
    public bool CanNavigateBack
    {
        get => _canNavigateBack;
        set => SetProperty(ref _canNavigateBack, value);
    }

    public BaseViewModel? CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }
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
