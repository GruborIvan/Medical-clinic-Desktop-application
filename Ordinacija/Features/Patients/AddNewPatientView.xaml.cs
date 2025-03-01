using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.Patients.Service;
using System.Windows;

namespace Ordinacija.Features.Patients
{
    /// <summary>
    /// Interaction logic for AddNewPatientView.xaml
    /// </summary>
    public partial class AddNewPatientView : Window
    {
        private readonly IPatientService _patientService;
        private readonly PatientsView _patientsView;

        public Patient CurrentPatient { get; private set; }
        private bool _isEditMode;

        public AddNewPatientView(
            IPatientService patientService,
            PatientsView patientView,
            Patient? patient = null)
        {
            InitializeComponent();
            _patientService = patientService;
            _patientsView = patientView;

            _isEditMode = patient != null;

            CurrentPatient = patient ?? new Patient();
            DataContext = CurrentPatient;

            this.Title = _isEditMode ? "Edit Patient" : "Add New Patient";
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            await (
                _isEditMode
                ? _patientService.UpdateExistingPatient(CurrentPatient)
                : _patientService.CreateNewPatient(CurrentPatient));

            await _patientsView.PatientViewModel.LoadPatients();

            this.Close();
        }

        private void PonistiButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Da li ste sigurni da želite da zatvorite stranicu? Imate nesačuvane izmene.",
                "Confirm Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
