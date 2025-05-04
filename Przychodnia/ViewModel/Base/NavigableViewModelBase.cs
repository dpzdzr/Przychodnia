using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Base;

public abstract class NavigableViewModelBase : ViewModelBase
{
    private readonly Stack<ViewModelBase> _navigationStack = new();
    private ViewModelBase? _currentViewModel;
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

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }
    protected void NavigateTo(ViewModelBase viewModel)
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
