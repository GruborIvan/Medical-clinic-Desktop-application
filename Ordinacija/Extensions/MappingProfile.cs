using AutoMapper;
using Ordinacija.Extensions.Resolvers;
using Ordinacija.Features.Patients.Models;

namespace Ordinacija.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PatientDbo, Patient>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom<FirstNameResolver>())
                .ForMember(dest => dest.LastName, opt => opt.MapFrom<LastNameResolver>())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom<BirthDateResolver>());
        }
    }
}
