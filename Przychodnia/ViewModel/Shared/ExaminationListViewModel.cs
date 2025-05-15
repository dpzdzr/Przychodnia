using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;
using Przychodnia.ViewModel.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.ViewModel.Shared;
public class ExaminationListViewModel : BaseListViewModel<ExaminationWrapper>
{
    public static string HeaderText => "Badania";
    public ExaminationListViewModel(INavigationService navigationService, IServiceProvider serviceProvider, IDialogService dialogService) : base(dialogService, navigationService, serviceProvider)
    {

    }
    public override Task InitializeAsync()
    {
        return Task.CompletedTask;
        //throw new NotImplementedException();
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

    protected override Task Remove()
    {
        throw new NotImplementedException();
    }
}
