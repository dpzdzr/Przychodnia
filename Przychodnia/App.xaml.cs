using System.Configuration;
using System.Data;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Data;
using Przychodnia.MappingProfile;
using Przychodnia.Repository.Implementation;
using Przychodnia.Repository.Interface;
using Przychodnia.Service.Implementation;
using Przychodnia.Service.Implementation.Entity;
using Przychodnia.Service.Interface;
using Przychodnia.Service.Interface.Entity;
using Przychodnia.View;
using Przychodnia.ViewModel.Admin;
using Przychodnia.ViewModel.Login;
using Przychodnia.ViewModel.Shared;

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

        private static void ConfigureServices(ServiceCollection services)
        {
            // DbContext
            services.AddDbContext<DbContext, AppDbContext>();

            //Messenger
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            // Mappers
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
            services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            // Services
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<NavigationServiceProxy>();
            services.AddSingleton<INavigationService>(provider 
                => provider.GetRequiredService<NavigationServiceProxy>());

            // Entities services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserTypeService, UserTypeService>();
            services.AddTransient<ILaboratoryService, LaboratoryService>();
            services.AddTransient<IPostalCodeService, PostalCodeService>();
            services.AddTransient<IPatientService, PatientService>();

            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<UserAddViewModel>();
            services.AddSingleton<AdminPanelViewModel>();
            services.AddTransient<UserListViewModel>();
            services.AddSingleton<HomePageViewModel>();
            services.AddTransient<UserEditViewModel>();
            services.AddTransient<PostalCodeListViewModel>();
            services.AddTransient<PatientAddViewModel>();
            services.AddTransient<PatientEditViewModel>();
            services.AddTransient<PatientListViewModel>();
            services.AddTransient<LaboratoryListViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var proxy = _serviceProvider.GetRequiredService<NavigationServiceProxy>();
            var mainViewModel = _serviceProvider.GetService<AdminPanelViewModel>();
            proxy.Current = mainViewModel;
            var mainView = new AdminPanelWindow(mainViewModel);
            mainView.ShowDialog();
        }
    }
}
