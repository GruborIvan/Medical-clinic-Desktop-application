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
using Ordinacija.Features.Login;
using Ordinacija.Features.MedicalReports;
using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.MedicalReports.Service.Implementation;
using Ordinacija.Features.MedicalReports.Repository;
using Ordinacija.Features.MedicalReports.Repository.Implementations;
using Ordinacija.Features.Doctors.Service;
using Ordinacija.Features.Doctors.Repository;
using Ordinacija.Features.Doctors.Service.Implementation;
using Ordinacija.Features.Doctors.Repository.Implementation;

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
            var loginWindow = _host.Services.GetRequiredService<LoginView>();
            loginWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register AutoMapper with the correct namespace
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(App));

            // Register services
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IPatientRepository, PatientRepository>();

            services.AddSingleton<IDoctorService, DoctorService>();
            services.AddSingleton<IDoctorRepository, DoctorRepository>();

            services.AddSingleton<IMedicalReportService, MedicalReportService>();
            services.AddSingleton<IMedicalReportRepository, MedicalReportRepository>();


            services.AddSingleton<PageNavigationService>();

            // Register ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<PatientViewModel>();
            services.AddTransient<MedicalReportViewModel>();

            // Register Views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<PatientsView>();
            services.AddSingleton<LoginView>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
