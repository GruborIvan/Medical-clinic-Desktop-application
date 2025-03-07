using Ordinacija.Features.Doctors.Models;
using Ordinacija.Features.Doctors.Repository;

namespace Ordinacija.Features.Doctors.Service.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task CreateNewDoctor(Doctor patient)
        {
            await _doctorRepository.InsertDoctor(patient);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return await _doctorRepository.GetAllDoctors();
        }

        public async Task UpdateExistingDoctor(Doctor patient)
        {
            await _doctorRepository.UpdateDoctor(patient);
        }
    }
}
