using Ordinacija.Features.Common;
using Ordinacija.Features.Patients;
using Ordinacija.Features.Patients.Service;
using Ordinacija.Helpers;

namespace Ordinacija
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IPatientService _patientService;
        private BaseViewModel _currentView;

        public RelayCommand ShowPatientsViewCommand { get; }

        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(); // Notify UI
            }
        }

        public MainViewModel(IPatientService patientService)
        {
            _patientService = patientService;

            CurrentView = new PatientViewModel(_patientService);
        }

        private void ShowPatientsView()
        {
            // Create new instance with dependency injection
            CurrentView = new PatientViewModel(_patientService);
        }
    }
}
