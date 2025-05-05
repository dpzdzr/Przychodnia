using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Interface;

public interface INavigationService
{
    void NavigateTo(ViewModelBase viewModel);
    void NavigateBack();
}

public interface IAdminNavigationService : INavigationService { }
