namespace Ordinacija.Features.MedicalReports.Models
{
    public class MedicalReportDbo
    {
        public string AcNalaz { get; set; }  // Šifra nalaza
        public string AcSubject { get; set; }  // Šifra pacijenta
        public DateTime AdDate { get; set; }  // Datum nalaza
        public string AcDescription { get; set; }  // Opis nalaza
        public string AcDG { get; set; }  // Dijagnoza
        public string AcTH { get; set; }  // TH
        public string AcKontrola { get; set; }  // Kontrola
        public string AcLekar { get; set; }  // Šifra lekara
    }
}
