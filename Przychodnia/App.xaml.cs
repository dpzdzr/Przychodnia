using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Data;
using Przychodnia.Repository.Implementation;
using Przychodnia.Repository.Interface;
using Przychodnia.Service;
using Przychodnia.View;
using Przychodnia.ViewModel;
using Przychodnia.Views;

namespace Przychodnia;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _serviceProvider;

    public App()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        // DbContext
        services.AddDbContext<AppDbContext>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Services
        services.AddSingleton<IUserSessionService, UserSessionService>();

        // ViewModels
        services.AddTransient<LoginViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        //var loginViewModel = _serviceProvider.GetService<LoginViewModel>();
        //var loginWindow = new LoginWindow
        //{
        //    DataContext = loginViewModel
        //};
        //loginWindow.Show();

        var adminPanelViewModel = _serviceProvider.GetService<AdminPanelViewModel>();
        var adminPanelView = new AdminPanelView
        {
            DataContext = adminPanelViewModel
        };

        adminPanelView.Show();
    }
}

