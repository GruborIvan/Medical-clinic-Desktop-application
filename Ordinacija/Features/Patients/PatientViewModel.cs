using Ordinacija.Features.Common;
using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.Patients.Service;
using Ordinacija.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Ordinacija.Features.Patients
{
    public class PatientViewModel : BaseViewModel
    {
        private readonly IPatientService _patientService;

        public ObservableCollection<Patient> Patients { get; set; } = new();

        public ICommand LoadPatientsCommand { get; }

        public PatientViewModel(IPatientService patientService)
        {
            _patientService = patientService;
            LoadPatientsCommand = new RelayCommand(LoadPatients);
            _ = LoadPatients();
        }

        private async Task LoadPatients()
        {
            var patients = await _patientService.GetAllPatients();
            Patients.Clear();
            foreach (var patient in patients)
            {
                Patients.Add(patient);
            }
        }
    }
}
