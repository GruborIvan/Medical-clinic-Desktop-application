using Ordinacija.Features.Doctors.Service;
using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.Patients.Models;
using System.Windows;

namespace Ordinacija.Features.MedicalReports
{
    /// <summary>
    /// Interaction logic for MedicalReportsView.xaml
    /// </summary>
    public partial class MedicalReportsView : Window
    {
        public MedicalReportViewModel MedicalReportViewModel;

        private readonly Patient _patient;
        private readonly IMedicalReportService _medicalReportService;
        private readonly IDoctorService _doctorService;

        public MedicalReportsView(
            IMedicalReportService medicalReportService,
            IDoctorService doctorsService,
            Patient? patient = null)
        {
            InitializeComponent();
            MedicalReportViewModel = new MedicalReportViewModel(patient, medicalReportService);

            _patient = patient;
            _doctorService = doctorsService;
            _medicalReportService = medicalReportService ?? throw new ArgumentNullException(nameof(medicalReportService));

            DataContext = MedicalReportViewModel;
        }

        private void AddNewMedicalReportButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewMedicalReportView = new AddNewMedicalReportView(_medicalReportService, _doctorService, this, _patient);
            addNewMedicalReportView.ShowDialog();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
