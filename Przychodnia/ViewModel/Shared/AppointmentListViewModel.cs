using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;

public partial class AppointmentListViewModel : BaseViewModel
{
    private readonly IAppointmentService _appointmentService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditCommand))]
    [NotifyCanExecuteChangedFor(nameof(CancelSelectionCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
    [NotifyCanExecuteChangedFor(nameof(AddCommand))]
    private AppointmentWrapper? selectedAppointment;
    [ObservableProperty] private ObservableCollection<AppointmentWrapper> appointments = [];

    public AppointmentListViewModel(IAppointmentService appointmentService, IDialogService dialogService) : base(dialogService)
    {
        _appointmentService = appointmentService;

        //CancelCommand = new RelayCommand(CancelSelection, () => IsAnySelected);
        AddCommand = new AsyncRelayCommand(AddAppointment, () => IsAnySelected);
        RemoveCommand = new AsyncRelayCommand(RemoveAppointment, () => IsAnySelected);
        EditCommand = new AsyncRelayCommand(EditAppointment, () => IsAnySelected);
    }

    //IRelayCommand CancelCommand { get; }
    IAsyncRelayCommand AddCommand { get; }
    IAsyncRelayCommand EditCommand { get; }
    IAsyncRelayCommand RemoveCommand { get; }

    public async Task InitializeAsync()
    {
        var items =  await _appointmentService.GetAllWithDetailsAsync();
        items = [.. items.Select(a => new AppointmentWrapper(a))];
    }

    private bool IsAnySelected => SelectedAppointment is not null;

    [RelayCommand(CanExecute = nameof(IsAnySelected))]
    private void CancelSelection() => SelectedAppointment = null;
    private async Task AddAppointment()
    {
        throw new NotImplementedException();
    }
    private async Task EditAppointment()
    {
        throw new NotImplementedException();
    }
    private async Task RemoveAppointment()
    {
        throw new NotImplementedException();
    }
}
