using Ordinacija.Features.Patients.Models;

namespace Ordinacija.Features.Patients.Service
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task CreateNewPatient(Patient patient);
        Task UpdateExistingPatient(Patient patient);
    }
}
