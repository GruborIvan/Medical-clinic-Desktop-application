using Ordinacija.Features.Patients.Models;

namespace Ordinacija.Features.Patients.Repository
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients();
    }
}
