using Ordinacija.Features.Doctors.Service;
using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.ReportPrint;
using Ordinacija.Features.ReportPrint.Repository;
using Ordinacija.Features.ReportPrint.Repository.Implementation;
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
        private readonly ISchemaRepository _schemaRepository;

        public MedicalReportsView(
            IMedicalReportService medicalReportService,
            IDoctorService doctorsService,
            ISchemaRepository schemaRepository,
            Patient? patient = null)
        {
            InitializeComponent();
            MedicalReportViewModel = new MedicalReportViewModel(patient, medicalReportService, doctorsService);

            _patient = patient;
            _doctorService = doctorsService;
            _schemaRepository = schemaRepository;
            _medicalReportService = medicalReportService ?? throw new ArgumentNullException(nameof(medicalReportService));

            DataContext = MedicalReportViewModel;
        }

        private void AddNewMedicalReportButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewMedicalReportView = new AddNewMedicalReportView(
                _medicalReportService, 
                _doctorService, 
                _schemaRepository,
                this, 
                _patient, 
                false
            );

            addNewMedicalReportView.ShowDialog();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedicalReportViewModel.CurrentMedicalReport == null)
            {
                return;
            }

            var printPdfRepositor = new PdfPrintService();
            printPdfRepositor.PrintMedicalReport(MedicalReportViewModel.CurrentMedicalReport, _patient);
        }

        private void AddNewUZButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewUZView = new AddNewMedicalReportView(
                _medicalReportService, 
                _doctorService, 
                _schemaRepository,
                this, 
                _patient, 
                true
            );
            
            addNewUZView.ShowDialog();
        }

        private void EditMedicalReportButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewMedicalReportView = new AddNewMedicalReportView(
                _medicalReportService,
                _doctorService, 
                _schemaRepository, 
                this, 
                _patient, 
                false, 
                MedicalReportViewModel.CurrentMedicalReport
            );

            addNewMedicalReportView.ShowDialog();
        }
    }
}
