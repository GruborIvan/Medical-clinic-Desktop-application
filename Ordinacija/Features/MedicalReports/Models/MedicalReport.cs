namespace Ordinacija.Features.MedicalReports.Models
{
    public class MedicalReport
    {
        public string ReportId { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime DateOfReport { get; set; }
        public string Description { get; set; }
        public string DG { get; set; }
        public string TH { get; set; }
        public string Control { get; set; }
    }
}
