using Ordinacija.Features.Doctors.Models;

namespace Ordinacija.Features.Doctors.Repository
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task InsertDoctor(Doctor doctor);
        Task UpdateDoctor(Doctor doctor);
    }
}
