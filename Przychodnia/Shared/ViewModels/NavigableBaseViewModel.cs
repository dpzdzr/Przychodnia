using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.HomePage.ViewModels;
using Przychodnia.Shared.Services.DialogService;
using System.Windows.Controls;

namespace Przychodnia.Shared.ViewModels;

public abstract partial class BaseNavigableViewModel : BaseViewModel
{
    private readonly Stack<BaseViewModel> _navigationStack = new();
    protected readonly IServiceProvider _services;

    [ObservableProperty] private BaseViewModel? currentViewModel;
    [ObservableProperty] private bool canNavigateBack;
    [ObservableProperty] private bool isBusy;
    private readonly string _homePageCaption = string.Empty;

    public BaseNavigableViewModel(IDialogService dialogService, IServiceProvider services,
        string homePageCaption)
        : base(dialogService)
    {
        _homePageCaption = homePageCaption ?? string.Empty;
        _services = services;
        NavigateBackCommand = new RelayCommand(NavigateBack);
        CloseCommand = new RelayCommand(CloseApplication);
    }

    public event Action? CloseApplicationEvent;
    public IRelayCommand NavigateBackCommand { get; }
    public IRelayCommand CloseCommand { get; }

    public void NavigateTo(BaseViewModel viewModel)
    {
        if (CurrentViewModel != null)
            _navigationStack.Push(CurrentViewModel);

        CurrentViewModel = viewModel;
        UpdateNavigationState();
    }
    public void NavigateBack()
    {
        if (_navigationStack.Count > 0)
            CurrentViewModel = _navigationStack.Pop();
        UpdateNavigationState();
    }

    protected IAsyncRelayCommand CreateNavigationCommand<TViewModel>()
    where TViewModel : BaseViewModel
    {
        return new AsyncRelayCommand(() => NavigateToAsync<TViewModel>(vm => vm.InitializeAsync()));
    }
    protected void InitializeHomePage()
    {
        var homePage = _services.GetRequiredService<HomePageViewModel>();
        homePage.Caption = _homePageCaption;
        CurrentViewModel = homePage;
    }

    private async Task NavigateToAsync<TViewModel>(Func<TViewModel, Task>? initializer = null)
        where TViewModel : BaseViewModel
    {
        if (CurrentViewModel is TViewModel)
            return;

        IsBusy = true;
        try
        {
            var vm = _services.GetRequiredService<TViewModel>();
            if (initializer != null)
                await initializer(vm);
            NavigateTo(vm);
        }
        finally
        {
            IsBusy = false;
        }
    }
    private void UpdateNavigationState()
    {
        CanNavigateBack = _navigationStack.Count > 0;
    }
    private void CloseApplication()
    {
        CloseApplicationEvent?.Invoke();
    }
}
