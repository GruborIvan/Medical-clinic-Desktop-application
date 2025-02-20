using AutoMapper;
using Ordinacija.Features.Patients.Models;

namespace Ordinacija.Extensions.Resolvers
{
    public class FirstNameResolver : IValueResolver<PatientDbo, Patient, string>
    {
        public string Resolve(PatientDbo source, Patient destination, string member, ResolutionContext context)
        {
            return source.AcName2.Split(' ').FirstOrDefault();
        }
    }

    public class LastNameResolver : IValueResolver<PatientDbo, Patient, string>
    {
        public string Resolve(PatientDbo source, Patient destination, string member, ResolutionContext context)
        {
            return source.AcName2.Split(' ').LastOrDefault();
        }
    }

    public class BirthDateResolver : IValueResolver<PatientDbo, Patient, DateTime>
    {
        public DateTime Resolve(PatientDbo source, Patient destination, DateTime member, ResolutionContext context)
        {
            if (DateTime.TryParse(source.AcFieldSA, out var dateOfBirth))
            {
                return dateOfBirth;
            }

            return DateTime.MinValue;
        }
    }

}
