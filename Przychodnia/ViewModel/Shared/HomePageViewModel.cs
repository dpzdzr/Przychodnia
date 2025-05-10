using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Shared;

public partial class HomePageViewModel() : BaseViewModel
{
    [ObservableProperty] private string caption;
}
