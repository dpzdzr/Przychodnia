using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Features.Login.Services;
using Przychodnia.Features.Login.ViewModels;
using Przychodnia.Features.Panels.Admin.ViewModels;
using Przychodnia.Features.Panels.Admin.Views;
using Przychodnia.Shared.Services.NavigationService;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Przychodnia.Features.Login.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IPanelWindowFactory _windowFactory;

        public LoginWindow(IPanelWindowFactory windowFactory, LoginViewModel vm)
        {
            InitializeComponent();
            _windowFactory = windowFactory;

            vm.LoginSucceeded += OnLoginSucceeded;
            DataContext = vm;
        }

        private void OnLoginSucceeded(int userTypeId)
        {
            var window = _windowFactory.CreateFor(userTypeId);
            window.Show();

            this.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is not null)
                ((dynamic)DataContext).InputPassword = ((PasswordBox)sender).SecurePassword;
        }
    }
}
