using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.PatientFeature.Messages;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.PatientFeature.Wrappers;
using Przychodnia.Shared.Messages;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels;

public partial class PatientListViewModel : BaseListViewModel<PatientWrapper>
{
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    [ObservableProperty] private string selectedPatientFirstName = string.Empty;
    [ObservableProperty] private string selectedPatientLastName = string.Empty;
    [ObservableProperty] private string selectedPatientPesel = string.Empty;

    public PatientListViewModel(
        IAppointmentService appointmentService,
        INavigationService navigationService,
        IServiceProvider serviceProvider,
        IPatientService patientService,
        IDialogService dialogService,
        IMessenger messenger,
        IMapper mapper)
        : base(dialogService, navigationService, serviceProvider, messenger)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
        _mapper = mapper;

        _messenger.Register<PatientChangedMessage>(this, (r, m) => HandlePatientChangedMessage(m));
    }

    public static string HeaderText => "Pacjenci";

    public override async Task InitializeAsync()
    {
        var patients = await _patientService.GetAllWithDetailsAsync();
        _allItems = [.. patients.Select(p => new PatientWrapper(p))];
        Items = [.. _allItems];
    }

    protected override async Task Add()
    {
        var addVm = _serviceProvider.GetRequiredService<PatientAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
    }
    protected override void ClearFilter()
    {
        SelectedPatientFirstName = string.Empty;
        SelectedPatientLastName = string.Empty;
        SelectedPatientPesel = string.Empty;

        Items = [.. _allItems];
    }
    protected override async Task Edit()
    {
        var editVm = _serviceProvider.GetRequiredService<PatientEditViewModel>();
        if (SelectedItem is not null)
            await editVm.InitializeAsync(SelectedItem);
        _navigationService.NavigateTo(editVm);
    }
    protected override void Filter() => Items = [.. ApplyFilters()];
    protected override async Task Remove()
    {
        await TryExecuteAsync(async () =>
        {
            if (Confirm("Potwierdzenie usunięcia", "Czy na pewno chcesz usunąć wybranego pacjenta?"))
            {
                if (SelectedItem is null || SelectedItem.Id is not int id)
                    throw new InvalidOperationException("Nie można usunąć pacjenta bez ID");
                if (await _appointmentService.HasAppointmentsForPatient(id) &&
                    Confirm("Podejmij działanie", "Wybrany pacjent ma przypisane wizyty. " +
                    "Czy chcesz je usunąć razem z pacjentem?"))
                    await _appointmentService.RemoveAllForPatient(id);

                await _patientService.RemoveAsync(id);
                Items.Remove(SelectedItem);
            }
        });
    }

    private void HandlePatientChangedMessage(PatientChangedMessage message)
    {
        switch (message.Value.Action)
        {
            case EntityChangedAction.Added:
                HandleAdded((Patient)message.Value.Entity);
                break;
            case EntityChangedAction.Edited:
                HandleEdited((Patient)message.Value.Entity);
                break;
        }
    }
    private void HandleAdded(Patient entity)
    {
        Items.Add(new(entity));
    }
    private void HandleEdited(Patient entity)
    {
        var current = Items.First(p => p.Id == entity.Id);
        _mapper.Map(entity, current);
    }
    private IEnumerable<PatientWrapper> ApplyFilters()
    {
        var query = _allItems?.AsEnumerable() ?? [];

        query = FilterByStringAttribute(query, p => p.FirstName, SelectedPatientFirstName);
        query = FilterByStringAttribute(query, p => p.LastName, SelectedPatientLastName);
        query = FilterByStringAttribute(query, p => p.Pesel, SelectedPatientPesel);

        return query;
    }
}
