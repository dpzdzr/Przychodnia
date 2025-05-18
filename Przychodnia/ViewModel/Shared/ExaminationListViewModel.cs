using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Form;
using Przychodnia.ViewModel.Wrapper;

namespace Przychodnia.ViewModel.Shared;
public class ExaminationListViewModel : BaseListViewModel<ExaminationWrapper>
{
    private readonly IExaminationService _examinationService;
    public static string HeaderText => "Badania";
    public ExaminationListViewModel(IExaminationService examinationService, INavigationService navigationService, IServiceProvider serviceProvider, IDialogService dialogService) : base(dialogService, navigationService, serviceProvider)
    {
        _examinationService = examinationService;
    }
    public override async Task InitializeAsync()
    {
        var items = await _examinationService.GetAllWithDetailsAsync();
        Items = [.. items.Select(e => new ExaminationWrapper(e))];
    }

    protected override async Task Add()
    {
        var addVm = _serviceProvider.GetRequiredService<ExaminationAddViewModel>();
        await addVm.InitializeAsync();
        _navigationService.NavigateTo(addVm);
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

    protected override Task Remove()
    {
        throw new NotImplementedException();
    }
}
