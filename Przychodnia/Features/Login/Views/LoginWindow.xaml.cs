using CommunityToolkit.Mvvm.Messaging;
using Przychodnia.Features.Login.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Przychodnia.Features.Login.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IMessenger _messenger;

        public LoginWindow(LoginViewModel vm, IMessenger messenger)
        {
            InitializeComponent();

            _messenger = messenger;

            vm.RequestClose += () => Close();
            DataContext = vm;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is not null)
                ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword;
        }
    }
}
