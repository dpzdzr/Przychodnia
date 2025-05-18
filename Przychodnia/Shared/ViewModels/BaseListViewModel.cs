using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Shared.Services;
using Przychodnia.ViewModel.Base;
using System.Collections.ObjectModel;

namespace Przychodnia.Shared.ViewModels;

public abstract partial class BaseListViewModel<TWrapper> : BaseViewModel
    where TWrapper : BaseWrapper
{
    protected readonly INavigationService _navigationService;
    protected readonly IServiceProvider _serviceProvider;

    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
    [NotifyCanExecuteChangedFor(nameof(CancelCommand))]
    [NotifyPropertyChangedFor(nameof(IsItemSelected))]
    [ObservableProperty] private TWrapper? selectedItem;
    [ObservableProperty] private ObservableCollection<TWrapper> items = [];
    protected List<TWrapper> _allItems = [];

    public BaseListViewModel(IDialogService dialogService, INavigationService navigationService,
        IServiceProvider serviceProvider) : base(dialogService)
    {
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;

        AddCommand = new AsyncRelayCommand(Add);
        EditCommand = new AsyncRelayCommand(Edit, () => IsItemSelected);
        RemoveCommand = new AsyncRelayCommand(Remove, () => IsItemSelected);
        CancelCommand = new RelayCommand(Cancel, () => IsItemSelected);
        FilterCommand = new RelayCommand(Filter);
        ClearFilterCommand = new RelayCommand(ClearFilter);
    }

    public IAsyncRelayCommand AddCommand { get; }
    public IAsyncRelayCommand EditCommand { get; }
    public IAsyncRelayCommand RemoveCommand { get; }
    public IRelayCommand CancelCommand { get; }
    public IRelayCommand FilterCommand { get; }
    public IRelayCommand ClearFilterCommand { get; }

    public bool IsItemSelected => SelectedItem is not null;

    public abstract Task InitializeAsync();

    protected abstract Task Add();
    protected abstract Task Edit();
    protected abstract Task Remove();
    protected abstract void Filter();
    protected abstract void ClearFilter();
    protected static IEnumerable<TWrapper> FilterByStringAttribute
        (IEnumerable<TWrapper> source, Func<TWrapper, string?> selector, string? filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
            return source;

        var pattern = filter.Trim();

        return source.Where(u => !string.IsNullOrWhiteSpace(selector(u)) &&
                            selector(u)!.StartsWith(pattern, StringComparison.OrdinalIgnoreCase));
    }

    private void Cancel() => SelectedItem = null;
}
