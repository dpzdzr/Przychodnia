using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Przychodnia.Data;
using Przychodnia.Features.Entities.AppointmentFeature.Mapping;
using Przychodnia.Features.Entities.AppointmentFeature.Repositories;
using Przychodnia.Features.Entities.AppointmentFeature.Services;
using Przychodnia.Features.Entities.AppointmentFeature.ViewModels;
using Przychodnia.Features.Entities.LaboratoryFeature.Repositories;
using Przychodnia.Features.Entities.LaboratoryFeature.Services;
using Przychodnia.Features.Entities.LaboratoryFeature.ViewModels;
using Przychodnia.Features.Entities.PatientFeature.Repositories;
using Przychodnia.Features.Entities.PatientFeature.Services;
using Przychodnia.Features.Entities.PatientFeature.ViewModels;
using Przychodnia.Features.Entities.PostalCodeFeature.Repositories;
using Przychodnia.Features.Entities.PostalCodeFeature.Services;
using Przychodnia.Features.Entities.PostalCodeFeature.ViewModels;
using Przychodnia.Features.Entities.UserFeature.Repositories;
using Przychodnia.Features.Entities.UserFeature.Services;
using Przychodnia.Features.Entities.UserFeature.ViewModels;
using Przychodnia.Features.Entities.UserTypesFeature.Repositories;
using Przychodnia.Features.Entities.UserTypesFeature.Services;
using Przychodnia.Features.HomePage.ViewModels;
using Przychodnia.Features.Login.Services;
using Przychodnia.Features.Login.ViewModels;
using Przychodnia.Features.Login.Views;
using Przychodnia.Features.Panels.Admin.ViewModels;
using Przychodnia.Features.Panels.Admin.Views;
using Przychodnia.Features.Panels.Doctor.ViewModels;
using Przychodnia.Features.Panels.Receptionist.ViewModels;
using Przychodnia.Shared.Services.CurrentUserService;
using Przychodnia.Shared.Services.DialogService;
using Przychodnia.Shared.Services.NavigationService;
using System.Windows;

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
            services.AddAutoMapper(typeof(AppointmentMappingProfile));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
            services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Services
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<NavigationServiceProxy>();
            services.AddSingleton<INavigationService>(provider
                => provider.GetRequiredService<NavigationServiceProxy>());
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            // Login services
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<IPanelWindowFactory, PanelWindowFactory>();
            services.AddSingleton<ILogoutService, LogoutService>();

            // Entities services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserLookupService, UserLookupService>();
            services.AddTransient<IUserTypeService, UserTypeService>();
            services.AddTransient<ILaboratoryService, LaboratoryService>();
            services.AddTransient<IPostalCodeService, PostalCodeService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IAppointmentLookupService, AppointmentLookupService>();

            // Panels viewModels
            services.AddSingleton<AdminPanelViewModel>();
            services.AddSingleton<ReceptionistPanelViewModel>();
            services.AddSingleton<DoctorPanelViewModel>();
            
            // Viewmodels
            services.AddSingleton<HomePageViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<UserAddViewModel>();
            services.AddTransient<UserListViewModel>();
            services.AddTransient<UserEditViewModel>();
            services.AddTransient<PostalCodeListViewModel>();
            services.AddTransient<PatientAddViewModel>();
            services.AddTransient<PatientEditViewModel>();
            services.AddTransient<PatientListViewModel>();
            services.AddTransient<LaboratoryListViewModel>();
            services.AddTransient<AppointmentListViewModel>();
            services.AddTransient<AppointmentAddViewModel>();
            services.AddTransient<AppointmentEditViewModel>();

            // Windows
            services.AddTransient<LoginWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            //var proxy = _serviceProvider.GetRequiredService<NavigationServiceProxy>();
            //var mainViewModel = _serviceProvider.GetService<AdminPanelViewModel>();
            //proxy.Current = mainViewModel;
            //var mainView = new AdminPanelWindow(mainViewModel);
            //mainView.ShowDialog();

            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }
}
