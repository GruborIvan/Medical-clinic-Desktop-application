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
            var parts = source.AcName2.Split(' ').Skip(1); // Skip the first element
            return string.Join(" ", parts);
        }
    }
}
