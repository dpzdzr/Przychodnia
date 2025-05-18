using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Shared.Services;

public interface INavigationService
{
    void NavigateTo(BaseViewModel viewModel);
    void NavigateBack();
}
