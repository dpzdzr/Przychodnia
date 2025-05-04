using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Data;
using Przychodnia.Repository.Implementation;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Implementation;
using Przychodnia.Service.Interface;
using Przychodnia.View;
using Przychodnia.ViewModel.Admin;
using Przychodnia.ViewModel.Login;

namespace Przychodnia
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // DbContext
            services.AddDbContext<DbContext, AppDbContext>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();

            // Services
            services.AddSingleton<IUserSessionService, UserSessionService>();
            services.AddSingleton<IDialogService, DialogService>();
            // Entities services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserTypeService, UserTypeService>();
            services.AddTransient<ILaboratoryService, LaboratoryService>();

            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<AddUserViewModel>();
            services.AddTransient<AdminPanelViewModel>();
            services.AddTransient<UsersListViewModel>();
            services.AddSingleton<AdminPanelHomePageViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainViewModel = _serviceProvider.GetService<AdminPanelViewModel>();
            var mainView = new MainWindow(mainViewModel);
            mainView.ShowDialog();
        }
    }
}
