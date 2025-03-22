using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.Patients.Models;

namespace Ordinacija.Features.ReportPrint.Repository
{
    public interface IPdfPrintService
    {
        void PrintMedicalReport(MedicalReport medicalReport, Patient patient);
    }
}
