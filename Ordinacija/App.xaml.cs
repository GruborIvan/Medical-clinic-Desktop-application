using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordinacija.Features.Patients.Service;
using Ordinacija.Features.Patients;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Ordinacija.Features.Patients.Repository;
using Ordinacija.Features.Common;
using AutoMapper;
using Ordinacija.Extensions;

namespace Ordinacija
{
    public partial class App : Application
    {
        private readonly IHost _host;
        public static IMapper Mapper { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        public App()
        {
            // Create and build the host with DI services.
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Configure services (services, ViewModels, and Views)
                    ConfigureServices(services);
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Configure appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // Retrieve AutoMapper service
            Mapper = _host.Services.GetRequiredService<IMapper>();

            // Get MainWindow and show
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register AutoMapper with the correct namespace
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(App));

            // Register services
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IPatientRepository, PatientRepository>();

            services.AddSingleton<PageNavigationService>();

            // Register ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<PatientViewModel>();

            // Register Views
            services.AddSingleton<MainWindow>();
            services.AddTransient<PatientsView>();

            services.AddSingleton<Func<PatientsView>>(provider => () => provider.GetRequiredService<PatientsView>());
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
