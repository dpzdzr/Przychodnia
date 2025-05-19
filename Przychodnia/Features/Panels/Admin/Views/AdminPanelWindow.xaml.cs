using Przychodnia.Features.Panels.Admin.ViewModels;
using System.Windows;

namespace Przychodnia.Features.Panels.Admin.Views;

public partial class AdminPanelWindow : Window
{
    public AdminPanelWindow(AdminPanelViewModel vm)
    {
        InitializeComponent();

        vm.CloseWindowEvent += Close;
        DataContext = vm;
    }
}