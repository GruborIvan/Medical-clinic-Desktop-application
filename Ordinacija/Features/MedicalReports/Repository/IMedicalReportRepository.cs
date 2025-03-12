using Ordinacija.Features.MedicalReports.Models;

namespace Ordinacija.Features.MedicalReports.Repository
{
    public interface IMedicalReportRepository
    {
        Task<IEnumerable<MedicalReport>> GetMedicalReportsForPatient(string patientId);
        Task InsertMedicalReport(MedicalReport medicalReport);
    }
}
