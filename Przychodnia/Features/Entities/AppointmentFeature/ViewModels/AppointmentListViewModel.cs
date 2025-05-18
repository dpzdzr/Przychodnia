using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.AppointmentFeature.Wrappers;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.Services.NavigationService;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.Entities.AppointmentFeature.ViewModels;

public partial class AppointmentListViewModel : BaseListViewModel<AppointmentWrapper>
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentListViewModel(IAppointmentService appointmentService, IDialogService dialogService,
        INavigationService navigationService, IServiceProvider serviceProvider)
        : base(dialogService, navigationService, serviceProvider)
    {
        _appointmentService = appointmentService;
    }

    public static string HeaderText => "Wizyty";

    public override async Task InitializeAsync()
    {
        var items = await _appointmentService.GetAllWithDetailsAsync();
        Items = [.. items.Select(a => new AppointmentWrapper(a))];
    }

    protected override async Task Add()
    {
        var addVm = _serviceProvider.GetRequiredService<AppointmentAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
    }

    protected override void ClearFilter()
    {
        throw new NotImplementedException();
    }

    protected override async Task Edit()
    {
        var editVm = _serviceProvider.GetRequiredService<AppointmentEditViewModel>();
        await editVm.InitializeAsync(SelectedItem!);
        _navigationService.NavigateTo(editVm);
    }

    protected override void Filter()
    {
        throw new NotImplementedException();
    }

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
}
