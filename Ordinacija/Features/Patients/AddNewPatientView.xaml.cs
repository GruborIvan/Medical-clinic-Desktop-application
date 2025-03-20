using Microsoft.IdentityModel.Tokens;
using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.Patients.Service;
using System.Windows;
using System.Windows.Input;

namespace Ordinacija.Features.Patients
{
    /// <summary>
    /// Interaction logic for AddNewPatientView.xaml
    /// </summary>
    public partial class AddNewPatientView : Window
    {
        private readonly IPatientService _patientService;
        private readonly PatientsView _patientsView;

        private const string _editPatientTitle = "Izmena podataka o pacijentu";
        private const string _addNewPatientTitle = "Unos novog pacijenta";

        public Patient CurrentPatient { get; private set; }

        public string TitleText { get; }
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

            var pageTitle = _isEditMode ? _editPatientTitle : _addNewPatientTitle;

            this.Title = pageTitle;
            TitleText = pageTitle;

            DataContext = this;
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatePatientEntry())
                return;

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
                "Da li ste sigurni da želite da zatvorite stranicu bez čuvanja izmena?",
                "Confirm Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private bool ValidatePatientEntry()
        {
            string errorMessage = string.Empty;

            if (CurrentPatient.FirstName.IsNullOrEmpty())
                errorMessage += "Obavezno je uneti ime pacijenta.\n";

            if  (CurrentPatient.LastName.IsNullOrEmpty())
                errorMessage += "Obavezno je uneti prezime pacijenta.\n";

            if (CurrentPatient.DateOfBirth == DateTime.MinValue)
                errorMessage += "Obavezno je uneti datum rodjenja pacijenta.\n";

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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Allow only numeric input
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
    }
}
