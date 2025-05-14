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
        var items =  await _appointmentService.GetAllWithDetailsAsync();
        Items = [.. items.Select(a => new AppointmentWrapper(a))];
    }

    protected override Task Add()
    {
        throw new NotImplementedException();
    }

    protected override void ClearFilter()
    {
        throw new NotImplementedException();
    }

    protected override Task Edit()
    {
        throw new NotImplementedException();
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
                
                Items.Remove(SelectedItem);
            }
        });
    }
}
