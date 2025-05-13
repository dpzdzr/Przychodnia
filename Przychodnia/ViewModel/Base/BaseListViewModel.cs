using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Service.Interface;

namespace Przychodnia.ViewModel.Base;

public abstract partial class BaseListViewModel<TWrapper> : BaseViewModel
    where TWrapper : BaseWrapper
{
    protected readonly INavigationService _navigationService;
    protected readonly IServiceProvider _serviceProvider;

    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
    [NotifyPropertyChangedFor(nameof(IsItemSelected))]
    [ObservableProperty] private TWrapper? selectedItem;
    [ObservableProperty] private ObservableCollection<TWrapper> items = [];

    public BaseListViewModel(IDialogService dialogService, INavigationService navigationService,
        IServiceProvider serviceProvider) : base(dialogService)
    {
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;

        AddCommand = new AsyncRelayCommand(Add);
        EditCommand = new AsyncRelayCommand(Edit, () => IsItemSelected);
        RemoveCommand = new AsyncRelayCommand(Remove, () => IsItemSelected);
        CancelCommand = new RelayCommand(Cancel, () => IsItemSelected);
    }

    public IAsyncRelayCommand AddCommand { get; }
    public IAsyncRelayCommand EditCommand { get; }
    public IAsyncRelayCommand RemoveCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public bool IsItemSelected => SelectedItem is not null;

    public abstract Task InitializeAsync();

    protected abstract Task Edit();
    protected abstract Task Remove();
    protected abstract Task Add();

    private void Cancel() => SelectedItem = null;
}
