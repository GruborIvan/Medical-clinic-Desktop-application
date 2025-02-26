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
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AdDateOfBirth))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.AcFieldSA))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AcAddress))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.AcFieldSC))
                .ForMember(dest => dest.FathersName, opt => opt.MapFrom(src => src.AcFieldSD))
                .ForMember(dest => dest.YearOfFathersBirth, opt => opt.MapFrom(src => src.AcFieldSF))
                .ForMember(dest => dest.MothersName, opt => opt.MapFrom(src => src.AcFieldSG))
                .ForMember(dest => dest.YearOfMothersBirth, opt => opt.MapFrom(src => src.AcFieldSI));


            // Map from Patient to PatientDbo
            CreateMap<Patient, PatientDbo>()
                .ForMember(dest => dest.AcSubject, opt => opt.MapFrom(src => src.Id.ToString())) // Assuming Subject is the Id in Patient
                .ForMember(dest => dest.AcName2, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.AcAddress, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.AdDateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd"))) // Format as string
                .ForMember(dest => dest.AcFieldSA, opt => opt.MapFrom(src => src.PlaceOfBirth))
                .ForMember(dest => dest.AcFieldSC, opt => opt.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.AcFieldSD, opt => opt.MapFrom(src => src.FathersName))
                .ForMember(dest => dest.AcFieldSE, opt => opt.MapFrom(src => "")) // You can add mapping for Father's Place of Birth if needed
                .ForMember(dest => dest.AcFieldSF, opt => opt.MapFrom(src => src.YearOfFathersBirth))
                .ForMember(dest => dest.AcFieldSG, opt => opt.MapFrom(src => src.MothersName))
                .ForMember(dest => dest.AcFieldSH, opt => opt.MapFrom(src => "")) // You can add mapping for Mother's Place of Birth if needed
                .ForMember(dest => dest.AcFieldSI, opt => opt.MapFrom(src => src.YearOfMothersBirth));
        }
    }
}
