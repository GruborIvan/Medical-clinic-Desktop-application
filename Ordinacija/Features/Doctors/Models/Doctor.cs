using Microsoft.EntityFrameworkCore.Metadata;

namespace Ordinacija.Features.Doctors.Models
{
    public class Doctor
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNo { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
