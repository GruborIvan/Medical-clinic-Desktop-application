using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.ReportPrint.Repository;
using Ordinacija.Features.ReportPrint.Repository.Implementation;
using System.Windows;

namespace Ordinacija.Features.ReportPrint
{
    /// <summary>
    /// Interaction logic for AddNewUZView.xaml
    /// </summary>
    public partial class AddNewUZView : Window
    {
        public string UZ_Results { get; set; }

        private readonly ISchemaRepository _schemaRepository;
        private readonly IMedicalReportService _medicalReportService;
        private Patient _patient;

        public AddNewUZView(
            ISchemaRepository schemaRepository,
            IMedicalReportService medicalReportService,
            Patient patient)
        {
            InitializeComponent();

            _schemaRepository = schemaRepository;
            _medicalReportService = medicalReportService;
            _patient = patient;

            InitializeAsync();

            DataContext = this;
        }

        private async void InitializeAsync()
        {
            try
            {
                UZ_Results = await _schemaRepository.GetTemplateSchemaByKey("UZ ABDOMENA I BUBREGA");

                Dispatcher.Invoke(() =>
                {
                    SchemaTextBox.Text = UZ_Results;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading template: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newUZMedicalReport = new MedicalReport()
            {
                Anamneza = SchemaTextBox.Text,
                PatientId = _patient.AcSubject,
                DoctorId = "000001",
                DateOfReport = DateTime.Now,
                Description = "/",
                DG = "/",
                TH = "/",
                Control = "/",
            };

            _medicalReportService.InsertMedicalReport(newUZMedicalReport);

            var pdfGeneratorService = new PdfPrintService();
            pdfGeneratorService.PrintUZ(this.SchemaTextBox.Text);

            this.Close();
        }
    }
}
