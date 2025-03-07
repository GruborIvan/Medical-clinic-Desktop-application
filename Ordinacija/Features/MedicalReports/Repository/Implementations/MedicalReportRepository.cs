using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Ordinacija.Features.MedicalReports.Models;
using System.Data.SqlClient;

namespace Ordinacija.Features.MedicalReports.Repository.Implementations
{
    public class MedicalReportRepository : IMedicalReportRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public MedicalReportRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString("PantheonDB");
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicalReport>> GetMedicalReportsForPatient(string patientId)
        {
            using var connection = new SqlConnection(_connectionString);
            
            var query = @"
                SELECT 
                    acNalaz, acSubject, adDate, acDescription, acDG, acTH, acKontrola, acLekar
                FROM _css_Nalaz
                WHERE acSubject = @AcSubject";

            var medicalReportsDbos = connection.Query<IEnumerable<MedicalReportDbo>>(query, new { AcSubject = "patient_id" }).ToList();

            return _mapper.Map<IEnumerable<MedicalReport>>(medicalReportsDbos);
        }
    }
}
