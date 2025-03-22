using Ordinacija.Features.Doctors.Models;
using Ordinacija.Features.Doctors.Service;
using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.ReportPrint.Repository.Implementation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ordinacija.Features.MedicalReports
{
    /// <summary>
    /// Interaction logic for AddNewMedicalReportView.xaml
    /// </summary>
    public partial class AddNewMedicalReportView : Window
    {
        private readonly IMedicalReportService _medicalReportService;
        private readonly IDoctorService _doctorService;
        private readonly Patient _patient;

        private bool _isEditMode; 
        private string _currentlySelectedDoctorId = string.Empty;
        private readonly MedicalReportsView _medicalReportsView;

        public MedicalReport CurrentMedicalReport { get; set; }
        public ObservableCollection<Doctor> Doctors { get; } = new();
        public string TitleText { get; }

        private const string _editMedicalReportTitle = "Izmena podataka o nalazu";
        private const string _addNewMedicalReportTitle = "Unos novog nalaza";

        public AddNewMedicalReportView(
            IMedicalReportService medicalReportService,
            IDoctorService doctorService,
            MedicalReportsView medicalReportsView,
            Patient patient,
            MedicalReport? medicalReport = null)
        {
            InitializeComponent();

            _medicalReportService = medicalReportService;
            _doctorService = doctorService;
            _patient = patient;

            _medicalReportsView = medicalReportsView;
            _isEditMode = medicalReport != null;

            var pageTitle = _isEditMode ? _editMedicalReportTitle : _addNewMedicalReportTitle;

            this.Title = pageTitle;
            TitleText = pageTitle;

            LoadDoctors();

            CurrentMedicalReport = medicalReport ?? new MedicalReport { PatientId = _patient.AcSubject };
            SetComboBoxText(CurrentMedicalReport.DoctorName);

            DataContext = this;
        }

        private async void LoadDoctors()
        {
            var doctorsList = await _doctorService.GetAllDoctors();
            Doctors.Clear();
            foreach (var doctor in doctorsList)
            {
                Doctors.Add(doctor);
            }
        }

        private async void ConfirmCreateMedicalReportButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateMedicalReportEntry())
                return;

            _currentlySelectedDoctorId = DoctorComboBox.Text.Length == 6 ? DoctorComboBox.Text : GetDoctorIdForDoctorName(DoctorComboBox.Text);
            CurrentMedicalReport.DoctorId = _currentlySelectedDoctorId;
            CurrentMedicalReport.DateOfReport = DateTime.Now;

            if (_isEditMode)
            {
                await _medicalReportService.UpdateMedicalReport(CurrentMedicalReport);
            }
            else
            {
                await _medicalReportService.InsertMedicalReport(CurrentMedicalReport);
            }

            await _medicalReportsView.MedicalReportViewModel.LoadMedicalReports();

            var shouldPrint = Dialog_DoYouWantToPrint();

            if (shouldPrint)
            {
                var printPdfRepositor = new PdfPrintService();
                CurrentMedicalReport.DoctorName = DoctorComboBox.Text;
                printPdfRepositor.PrintMedicalReport(CurrentMedicalReport, _patient);
            }

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is Doctor selectedDoctor)
            {
                _currentlySelectedDoctorId = selectedDoctor.Id;
            }
        }

        private void ComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        public void SetComboBoxText(string text)
        {
            var comboBox = FindName("DoctorComboBox") as ComboBox;
            if (comboBox != null)
            {
                comboBox.Text = text;
            }
        }

        public bool ValidateMedicalReportEntry()
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(CurrentMedicalReport.Anamneza))
                errorMessage += "Obavezno je uneti anamnezu i fizikalni nalaz.\n";

            if (string.IsNullOrEmpty(CurrentMedicalReport.DG))
                errorMessage += "Obavezno je uneti dijagnozu.\n";

            if (string.IsNullOrEmpty(CurrentMedicalReport.TH))
                errorMessage += "Obavezno je uneti terapiju.\n";

            if (string.IsNullOrEmpty(CurrentMedicalReport.Control))
                errorMessage += "Obavezno je uneti kontrolu.\n";

            if (errorMessage != string.Empty)
            {
                MessageBox.Show(
                $"Potrebno je popuniti sva obavezna polja! \n {errorMessage}",
                "Nepotpun unos",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            }

            return errorMessage == string.Empty;
        }

        private bool Dialog_DoYouWantToPrint()
        {
            var messageBoxResult = MessageBox.Show(
                "Nalaz je uspešno sačuvan! Da li želite odmah da ga odštampate?",
                "Kreiran novi nalaz",
                MessageBoxButton.YesNo,     
                MessageBoxImage.Question
            );

            return messageBoxResult == MessageBoxResult.Yes;
        }
        

        private string GetDoctorIdForDoctorName(string doctorName)
        {
            return Doctors.Where(d => d.FullName.Equals(doctorName)).FirstOrDefault()!.Id;
        }
    }
}
