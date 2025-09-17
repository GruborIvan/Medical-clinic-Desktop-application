using Ordinacija.Features.Doctors.Models;
using Ordinacija.Features.Doctors.Service;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Ordinacija.Features.Doctors
{
    /// <summary>
    /// Interaction logic for AddNewDoctorView.xaml
    /// </summary>
    public partial class AddNewDoctorView : Window
    {
        private bool _isEditMode;
        private readonly IDoctorService _doctorService;

        public readonly DoctorsView DoctorsView;
        public Doctor CurrentDoctor { get; }
        public string TitleText { get; }

        private const string _editDoctorTitle = "Izmena podataka o doktoru";
        private const string _addDoctorTitle = "Dodavanje novog doktora";

        public AddNewDoctorView(
            IDoctorService doctorService,
            DoctorsView doctorsView,
            Doctor? doctor = null)
        {
            InitializeComponent();

            _doctorService = doctorService;
            _isEditMode = doctor != null;

            DoctorsView = doctorsView;
            CurrentDoctor = doctor ?? new Doctor() { DateOfBirth = DateTime.Now} ;

            var pageTitle = _isEditMode ? _editDoctorTitle : _addDoctorTitle;

            this.Title = pageTitle;
            TitleText = pageTitle;

            DataContext = this;
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateDoctorEntry())
                return;

            await (
                _isEditMode
                ? _doctorService.UpdateExistingDoctor(CurrentDoctor)
                : _doctorService.CreateNewDoctor(CurrentDoctor));

            await DoctorsView.DoctorViewModel.LoadDoctors();

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

        private bool ValidateDoctorEntry()
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(CurrentDoctor.FirstName))
                errorMessage += "Obavezno je uneti ime doktora.\n";

            if (string.IsNullOrEmpty(CurrentDoctor.LastName))
                errorMessage += "Obavezno je uneti prezime doktora.\n";

            if (CurrentDoctor.DateOfBirth == DateTime.MinValue)
                errorMessage += "Obavezno je uneti datum rodjenja doktora.\n";

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
    }
}
