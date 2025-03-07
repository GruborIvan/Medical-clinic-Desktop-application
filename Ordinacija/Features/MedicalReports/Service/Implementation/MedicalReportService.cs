using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.MedicalReports.Repository;

namespace Ordinacija.Features.MedicalReports.Service.Implementation
{
    public class MedicalReportService : IMedicalReportService
    {
        private readonly IMedicalReportRepository _medicalReportRepository;

        public MedicalReportService(IMedicalReportRepository medicalReportRepository)
        {
            _medicalReportRepository = medicalReportRepository;
        }

        public async Task<IEnumerable<MedicalReport>> GetMedicalReportsForPatient(string patientId)
        {
            return await _medicalReportRepository.GetMedicalReportsForPatient(patientId);
        }
    }
}
