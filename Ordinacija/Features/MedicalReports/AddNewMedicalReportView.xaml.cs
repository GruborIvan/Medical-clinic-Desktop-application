using Ordinacija.Features.Doctors.Models;
using Ordinacija.Features.Doctors.Service;
using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.Patients.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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

        private string _currentlySelectedDoctorId = string.Empty;
        private readonly MedicalReportsView _medicalReportsView;

        public MedicalReport CurrentMedicalReport { get; set; }
        public ObservableCollection<Doctor> Doctors { get; } = new();

        public AddNewMedicalReportView(
            IMedicalReportService medicalReportService,
            IDoctorService doctorService,
            MedicalReportsView medicalReportsView,
            Patient patient)
        {
            InitializeComponent();

            _medicalReportService = medicalReportService;
            _doctorService = doctorService;
            _patient = patient;

            _medicalReportsView = medicalReportsView;
            CurrentMedicalReport = new MedicalReport { PatientId = _patient.AcSubject };

            DataContext = this;
            LoadDoctors();
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
            CurrentMedicalReport.DoctorId = _currentlySelectedDoctorId;
            CurrentMedicalReport.DateOfReport = DateTime.Now;

            if (CurrentMedicalReport == null)
            {
                MessageBox.Show("Please fill in the required fields.");
                return;
            }

            await _medicalReportService.InsertMedicalReport(CurrentMedicalReport);

            await _medicalReportsView.MedicalReportViewModel.LoadMedicalReports();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is Doctor selectedDoctor)
            {
                _currentlySelectedDoctorId = selectedDoctor.Id;
            }
        }
    }
}
