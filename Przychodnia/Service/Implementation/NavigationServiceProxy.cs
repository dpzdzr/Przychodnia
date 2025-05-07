using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.Service.Implementation;

public class NavigationServiceProxy : INavigationService
{   
    public INavigationService Current { get; set; }
    public void NavigateBack()
        => Current?.NavigateBack();

    public void NavigateTo(BaseViewModel viewModel) 
        => Current?.NavigateTo(viewModel);
}
