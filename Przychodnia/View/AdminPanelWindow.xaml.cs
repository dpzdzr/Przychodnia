using Przychodnia.ViewModel.Admin;
using System.Windows;

namespace Przychodnia
{
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow(AdminPanelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}