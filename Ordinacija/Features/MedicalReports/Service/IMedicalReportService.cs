using Ordinacija.Features.MedicalReports.Models;

namespace Ordinacija.Features.MedicalReports.Service
{
    public interface IMedicalReportService
    {
        Task<IEnumerable<MedicalReport>> GetMedicalReportsForPatient(string patientId);
        Task InsertMedicalReport(MedicalReport medicalReport);
        Task UpdateMedicalReport(MedicalReport medicalReport);
    }
}
