using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Service.Interface;
using Przychodnia.ViewModel.Base;

namespace Przychodnia.ViewModel.Shared;

public partial class HomePageViewModel(IDialogService dialogService) 
    : BaseViewModel(dialogService)
{
    [ObservableProperty] private string caption = string.Empty;
}
