namespace Ordinacija.Features.Patients.Models
{
    public class PatientDbo
    {
        public string AcSubject { get; set; }      // Šifra
        public string AcName2 { get; set; }        // Prezime i ime
        public string AcAddress { get; set; }      // Adresa
        public string AdDateOfBirth { get; set; }
        public string AcFieldSA { get; set; }      // Mesto rođenja
        public string AcFieldSC { get; set; }      // Telefon
        public string AcFieldSD { get; set; }      // Ime oca
        public string AcFieldSE { get; set; }      // Mesto rođenja oca
        public int AcFieldSF { get; set; }         // Godište oca
        public string AcFieldSG { get; set; }      // Ime majke
        public string AcFieldSH { get; set; }      // Mesto rođenja majke
        public int AcFieldSI { get; set; }         // Godište majke
    }
}
