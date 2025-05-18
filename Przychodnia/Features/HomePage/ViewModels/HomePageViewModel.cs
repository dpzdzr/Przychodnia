using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Shared.Services;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.HomePage.ViewModels;

public partial class HomePageViewModel(IDialogService dialogService) 
    : BaseViewModel(dialogService)
{
    [ObservableProperty] private string caption = string.Empty;
}
