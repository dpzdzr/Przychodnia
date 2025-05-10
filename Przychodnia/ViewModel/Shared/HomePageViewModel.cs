using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Shared;

public class HomePageViewModel() : BaseViewModel
{
    private string _caption;

    public string Caption
    {
        get => _caption;
        set => SetProperty(ref _caption, value);
    }
}
