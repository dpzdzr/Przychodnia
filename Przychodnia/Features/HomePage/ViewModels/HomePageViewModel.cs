using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.ViewModels;

namespace Przychodnia.Features.HomePage.ViewModels;

public partial class HomePageViewModel(IDialogService dialogService, IMessenger messenger)
    : BaseViewModel(dialogService, messenger)
{
    [ObservableProperty] private string caption = string.Empty;
}
