using AutoMapper;
using Ordinacija.Extensions.Resolvers;
using Ordinacija.Features.Doctors.Models;
using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.Patients.Models;

namespace Ordinacija.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PatientDbo, Patient>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom<FirstNameResolver<PatientDbo>>())
                .ForMember(dest => dest.LastName, opt => opt.MapFrom<LastNameResolver<PatientDbo>>())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AdBirthDate))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.AcFieldSA))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AcAddress))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.AcFieldSC))
                .ForMember(dest => dest.FathersName, opt => opt.MapFrom(src => src.AcFieldSD))
                .ForMember(dest => dest.YearOfFathersBirth, opt => opt.MapFrom(src => src.AcFieldSF))
                .ForMember(dest => dest.MothersName, opt => opt.MapFrom(src => src.AcFieldSG))
                .ForMember(dest => dest.YearOfMothersBirth, opt => opt.MapFrom(src => src.AcFieldSI));


            // Map from Patient to PatientDbo
            CreateMap<Patient, PatientDbo>()
                .ForMember(dest => dest.AcSubject, opt => opt.MapFrom(src => src.AcSubject)) 
                .ForMember(dest => dest.AcName2, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.AcAddress, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.AdBirthDate, opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd"))) 
                .ForMember(dest => dest.AcFieldSA, opt => opt.MapFrom(src => src.PlaceOfBirth))
                .ForMember(dest => dest.AcFieldSC, opt => opt.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.AcFieldSD, opt => opt.MapFrom(src => src.FathersName))
                .ForMember(dest => dest.AcFieldSE, opt => opt.MapFrom(src => "")) 
                .ForMember(dest => dest.AcFieldSF, opt => opt.MapFrom(src => src.YearOfFathersBirth))
                .ForMember(dest => dest.AcFieldSG, opt => opt.MapFrom(src => src.MothersName))
                .ForMember(dest => dest.AcFieldSH, opt => opt.MapFrom(src => "")) 
                .ForMember(dest => dest.AcFieldSI, opt => opt.MapFrom(src => src.YearOfMothersBirth));

            CreateMap<DoctorDbo, Doctor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AcSubject))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom<FirstNameResolver<DoctorDbo>>())
                .ForMember(dest => dest.LastName, opt => opt.MapFrom<LastNameResolver<DoctorDbo>>())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.AdBirthDate))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.AcFieldSC));

            CreateMap<Doctor, DoctorDbo>()
                .ForMember(dest => dest.AcSubject, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AcName2, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.AdBirthDate, opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.AcFieldSC, opt => opt.MapFrom(src => src.PhoneNo));


            CreateMap<MedicalReportDbo, MedicalReport>()
            .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.AcNalaz))
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.AcSubject))
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.AcLekar))
            .ForMember(dest => dest.DateOfReport, opt => opt.MapFrom(src => src.AdDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.AcDescription))
            .ForMember(dest => dest.DG, opt => opt.MapFrom(src => src.AcDG))
            .ForMember(dest => dest.TH, opt => opt.MapFrom(src => src.AcTH))
            .ForMember(dest => dest.Control, opt => opt.MapFrom(src => src.AcKontrola));

            CreateMap<MedicalReport, MedicalReportDbo>()
                .ForMember(dest => dest.AcNalaz, opt => opt.MapFrom(src => src.ReportId))
                .ForMember(dest => dest.AcSubject, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.AcLekar, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.AdDate, opt => opt.MapFrom(src => src.DateOfReport))
                .ForMember(dest => dest.AcDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.AcDG, opt => opt.MapFrom(src => src.DG))
                .ForMember(dest => dest.AcTH, opt => opt.MapFrom(src => src.TH))
                .ForMember(dest => dest.AcKontrola, opt => opt.MapFrom(src => src.Control));
        }
    }
}
