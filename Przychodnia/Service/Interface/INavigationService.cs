using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.Service.Interface;

public interface INavigationService
{
    void NavigateTo(BaseViewModel viewModel);
    void NavigateBack();
}
