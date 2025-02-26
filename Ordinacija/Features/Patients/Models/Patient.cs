namespace Ordinacija.Features.Patients.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public string PlaceOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string FathersName { get; set; }
        public int YearOfFathersBirth { get; set; }
        public string MothersName { get; set; }
        public int YearOfMothersBirth { get; set; }
    }
}
