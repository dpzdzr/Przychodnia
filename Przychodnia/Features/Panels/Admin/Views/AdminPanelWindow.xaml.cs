using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Przychodnia.Features.Panels.Admin.ViewModels;

namespace Przychodnia.Features.Panels.Admin.Views;

public partial class AdminPanelWindow : Window
{
    public AdminPanelWindow(AdminPanelViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}