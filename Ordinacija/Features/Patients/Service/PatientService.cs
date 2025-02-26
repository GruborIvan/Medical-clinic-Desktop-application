using Ordinacija.Features.Patients.Models;
using Ordinacija.Features.Patients.Repository;

namespace Ordinacija.Features.Patients.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task CreateNewPatient(Patient patient)
        {
            await _patientRepository.InsertPatient(patient);
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _patientRepository.GetAllPatients();
        }

        public async Task UpdateExistingPatient(Patient patient)
        {
            await _patientRepository.UpdatePatient(patient);
        }
    }
}
