using Ordinacija.Features.Doctors.Models;
using Ordinacija.Features.Doctors.Service;
using Ordinacija.Features.MedicalReports;
using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.ReportPrint.Repository;
using Ordinacija.Features.ReportPrint.Repository.Implementation;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ordinacija.Features.ReportPrint
{
    /// <summary>
    /// Interaction logic for AddNewUZView.xaml
    /// </summary>
    public partial class AddNewUZView : Window
    {
        public string UZ_Results { get; set; }
        public ObservableCollection<Doctor> Doctors { get; } = new();

        private readonly MedicalReportsView _medicalReportsView;

        private readonly ISchemaRepository _schemaRepository;
        private readonly IMedicalReportService _medicalReportService;
        private readonly IDoctorService _doctorService;

        private Patient _patient;
        private string _currentlySelectedDoctorId = string.Empty;

        public AddNewUZView(
            ISchemaRepository schemaRepository,
            IMedicalReportService medicalReportService,
            IDoctorService doctorService,
            MedicalReportsView medicalReportsView,
            Patient patient)
        {
            InitializeComponent();
            _medicalReportsView = medicalReportsView;

            _schemaRepository = schemaRepository;
            _medicalReportService = medicalReportService;
            _doctorService = doctorService;
            _patient = patient;

            InitializeAsync();

            DataContext = this;
        }

        private async void InitializeAsync()
        {
            try
            {
                UZ_Results = await _schemaRepository.GetTemplateSchemaByKey("UZ ABDOMENA I BUBREGA");
                var doctors = await _doctorService.GetAllDoctors();

                foreach (var doctor in doctors)
                    Doctors.Add(doctor);

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

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        { 
            SaveButton.IsEnabled = false; 

            var newUZMedicalReport = new MedicalReport()
            {
                Anamneza = SchemaTextBox.Text,
                PatientId = _patient.AcSubject,
                DoctorId = _currentlySelectedDoctorId,
                DateOfReport = DateTime.Now,
                Description = "/",
                DG = "/",
                TH = "/",
                Control = "/",
            };

            await _medicalReportService.InsertMedicalReport(newUZMedicalReport);

            var pdfGeneratorService = new PdfPrintService();
            pdfGeneratorService.PrintUZ(this.SchemaTextBox.Text);

            await _medicalReportsView.MedicalReportViewModel.LoadMedicalReports();

            SaveButton.IsEnabled = true;
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is Doctor selectedDoctor)
            {
                _currentlySelectedDoctorId = selectedDoctor.Id;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
