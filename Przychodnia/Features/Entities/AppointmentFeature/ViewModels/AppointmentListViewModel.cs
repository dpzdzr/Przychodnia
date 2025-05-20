using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.AppointmentFeature.Messages;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.AppointmentFeature.Wrappers;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;
using System.Threading.Tasks;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels;

public partial class AppointmentListViewModel : BaseListViewModel<AppointmentWrapper>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;
    private List<AppointmentWrapper> _allAppointments = [];

    [ObservableProperty] private string patientFullNameFilter = string.Empty;
    [ObservableProperty] private string doctorFullNameFilter = string.Empty;
    [ObservableProperty] private string patientPeselFilter = string.Empty;
    [ObservableProperty] private DateTime? dateFilter;

    public AppointmentListViewModel(
        IAppointmentService appointmentService, 
        IDialogService dialogService,
        INavigationService navigationService, 
        IServiceProvider serviceProvider, 
        IMessenger messenger,
        IMapper mapper)
        : base(dialogService, navigationService, serviceProvider, messenger)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;

        _messenger.Register<AppointmentChangedMessage>(this, (r, m) => HandleAppointmentChangedMessage(m));
    }

    public static string HeaderText => "Wizyty";

    public override async Task InitializeAsync()
    {
        var items = await _appointmentService.GetAllWithDetailsAsync();
        _allAppointments = [.. items.Select(a => new AppointmentWrapper(a))];
        Items = [.. _allAppointments];
    }

    protected override async Task Add()
    {
        var addVm = _serviceProvider.GetRequiredService<AppointmentAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
    }
    protected override void ClearFilter()
    {
        Items = [.. _allAppointments];
        DoctorFullNameFilter = string.Empty;
        PatientPeselFilter = string.Empty;
        PatientFullNameFilter = string.Empty;
        DateFilter = null;
    }
    protected override async Task Edit()
    {
        var editVm = _serviceProvider.GetRequiredService<AppointmentEditViewModel>();
        await editVm.InitializeAsync(SelectedItem!);
        _navigationService.NavigateTo(editVm);
    }
    protected override void Filter() => Items = [.. ApplyFilters()];
    protected async override Task Remove()
    {
        await TryExecuteAsync(async () =>
        {
            if (Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybraną wizytę?"))
            {
                if (SelectedItem is null || SelectedItem.Id is not int id)
                    throw new InvalidOperationException("Nie można usunąć wizyty bez ID");

                await _appointmentService.RemoveAsync(id);
                Items.Remove(SelectedItem);
            }
        });
    }

    private void HandleAppointmentChangedMessage(AppointmentChangedMessage message)
    {
        switch (message.Value.Action)
        {
            case EntityChangedAction.Added:
                HandleAdded((Appointment)message.Value.Entity);
                break;
            case EntityChangedAction.Edited:
                HandleEdited((Appointment)message.Value.Entity);
                break;
        }
    }
    private void HandleEdited(Appointment entity)
    {
        var current = Items.First(a => a.Id == entity.Id);
        _mapper.Map(entity, current);
    }
    private void HandleAdded(Appointment entity)
    {
        Items.Add(new(entity));
    }

    private IEnumerable<AppointmentWrapper> ApplyFilters()
    {
        var query = _allAppointments?.AsEnumerable() ?? [];

        query = FilterByStringAttribute(query, a => a.AttendingDoctor.FullName, DoctorFullNameFilter);
        query = FilterByStringAttribute(query, a => a.Patient.FullName, PatientFullNameFilter);
        query = FilterByStringAttribute(query, a => a.Patient.Pesel, PatientPeselFilter);

        if (DateFilter is DateTime dt)
            query = query.Where(a => a.OnlyDate == DateOnly.FromDateTime(dt));

        return query;
    }
}
