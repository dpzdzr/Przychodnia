using CommunityToolkit.Mvvm.ComponentModel;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.HomePage.ViewModels;

public partial class HomePageViewModel(IDialogService dialogService)
    : BaseViewModel(dialogService)
{
    [ObservableProperty] private string caption = string.Empty;
}
