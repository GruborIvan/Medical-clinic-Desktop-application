namespace Ordinacija.Features.MedicalReports.Exceptions
{
    public class MedicalReportConcurrencyException : Exception
    {
        public MedicalReportConcurrencyException(string message) : base(message) { }
    }
}
