using Ordinacija.Features.Doctors.Models;
using Ordinacija.Features.Doctors.Service;
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
    /// Interaction logic for DocumentsManagerView.xaml
    /// </summary>
    public partial class DocumentsManagerView : Window
    {
        public string FormText { get; set; }
        public ObservableCollection<Doctor> Doctors { get; } = new();
        public List<string> DocumentTypes { get; }

        private readonly PdfPrintService _pdfPrintService;
        private readonly IDoctorService _doctorService;
        private readonly ISchemaRepository _schemaRepository;

        private Patient _patient;
        private string _currentlySelectedDoctorId = string.Empty;

        public DocumentsManagerView(
            IDoctorService doctorService,
            ISchemaRepository schemaRepository,
            Patient patient)
        {
            InitializeComponent();

            _doctorService = doctorService;
            _schemaRepository = schemaRepository;
            _pdfPrintService = new PdfPrintService();
            _patient = patient;

            DocumentTypes = GetFileTypeOptions();

            InitializeAsync();

            DataContext = this;
        }

        private async void InitializeAsync()
        {
            try
            {
                //FormText = await _schemaRepository.GetTemplateSchemaByKey("UZ ABDOMENA I BUBREGA");
                var doctors = await _doctorService.GetAllDoctors();

                foreach (var doctor in doctors)
                    Doctors.Add(doctor);

                Dispatcher.Invoke(() =>
                {
                    SchemaTextBox.Text = FormText;
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

            var doctorName = GetDoctorFullNameById(_currentlySelectedDoctorId);

            var selectedFileTypeOption = DocumentTypesComboBox.Text;

            if (selectedFileTypeOption.Equals(""))
            {
                SaveButton.IsEnabled = true;
                return;
            }

            switch(selectedFileTypeOption)
            {
                case "Lekarska potvrda": 
                    _pdfPrintService.PrintDoctorsExemption(_patient.FullName);
                    break;
                case "Potvrda za predskolsku ustanovu":
                    _pdfPrintService.PrintPreSchoolApproval(_patient);
                    break;
                case "Preporuke za ishranu - Alergije":
                    _pdfPrintService.PrintAlergyConfirmation(_patient.FullName, doctorName);
                    break;
                default: 
                    break;

            }

            SaveButton.IsEnabled = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is Doctor selectedDoctor)
            {
                _currentlySelectedDoctorId = selectedDoctor.Id;
            }
        }

        private string GetDoctorFullNameById(string doctoId)
        {
            return Doctors.Where(d => d.Id.Equals(doctoId)).FirstOrDefault()!.FullName;
        }

        private void ComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<string> GetFileTypeOptions()
        {
            return new List<string>()
            {
                "Lekarska potvrda",
                "Potvrda za predskolsku ustanovu",
                "Preporuke za ishranu - Alergije"
            };
        }
    }
}
