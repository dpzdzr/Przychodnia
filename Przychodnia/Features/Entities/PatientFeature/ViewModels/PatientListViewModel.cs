using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.PatientFeature.Messages;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.PatientFeature.Wrappers;
using Przychodnia.Shared.Services;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.Entities.PatientFeature.ViewModels;

public partial class PatientListViewModel : BaseListViewModel<PatientWrapper>
{
    private readonly IPatientService _patientService;
    [ObservableProperty] private string selectedPatientFirstName = string.Empty;
    [ObservableProperty] private string selectedPatientLastName = string.Empty;
    [ObservableProperty] private string selectedPatientPesel = string.Empty;
    public PatientListViewModel(IPatientService patientService, INavigationService navigationService,
        IServiceProvider serviceProvider, IDialogService dialogService)
        : base(dialogService, navigationService, serviceProvider)
    {
        _patientService = patientService;

        WeakReferenceMessenger.Default.Register<PatientAddedMessage>(this, (r, m) =>
        {
            Items.Add(new PatientWrapper(m.Value));
        });
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
                await _patientService.RemoveAsync(id);
                Items.Remove(SelectedItem);
            }
        });
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
