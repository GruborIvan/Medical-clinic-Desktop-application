using Ordinacija.Features.Doctors.Models;

namespace Ordinacija.Features.Doctors.Service
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task CreateNewDoctor(Doctor patient);
        Task UpdateExistingDoctor(Doctor patient);
    }
}
