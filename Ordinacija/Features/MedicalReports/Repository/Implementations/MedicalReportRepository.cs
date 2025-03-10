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

            try
            {
                var medicalReportsDbos = (await connection.QueryAsync<MedicalReportDbo>(query, new { AcSubject = patientId })).ToList();

                // Fix: Ensure _mapper.Map is correct and compatible with IEnumerable<>
                return _mapper.Map<IEnumerable<MedicalReport>>(medicalReportsDbos);
            }
            catch (Exception e)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error fetching medical reports: {e.Message}");
                throw;
            }

        }
    }
}
