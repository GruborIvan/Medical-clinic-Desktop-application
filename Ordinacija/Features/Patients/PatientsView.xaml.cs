using Ordinacija.Features.Doctors;
using Ordinacija.Features.Doctors.Service;
using Ordinacija.Features.MedicalReports;
using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.Patients.Service;
using System.Windows;
using System.Windows.Controls;

namespace Ordinacija.Features.Patients
{
    /// <summary>
    /// Interaction logic for PatientsView.xaml
    /// </summary>
    public partial class PatientsView : Window
    {
        public PatientViewModel PatientViewModel;

        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public PatientsView(
            IPatientService patientService,
            IDoctorService doctorService)
        {
            InitializeComponent();

            PatientViewModel = new PatientViewModel(patientService);
            _patientService = patientService;
            _doctorService = doctorService;
            DataContext = PatientViewModel;
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            var addNewPatientView = new AddNewPatientView(_patientService, this);
            this.Close();
            addNewPatientView.ShowDialog();
        }

        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Patient selectedPatient)
            {
                var editPatientView = new AddNewPatientView(_patientService, this, selectedPatient);
                editPatientView.ShowDialog();
            }
        }

        private void OpenMedicalReports_Click(object sender, RoutedEventArgs e)
        {
            var medicalReportView = new MedicalReportsView();
            medicalReportView.Show();
        }

        private void Doctors_Click(object sender, RoutedEventArgs e)
        {
            var doctorsView = new DoctorsView(_doctorService, _patientService);
            doctorsView.Show();
        }
    }
}
