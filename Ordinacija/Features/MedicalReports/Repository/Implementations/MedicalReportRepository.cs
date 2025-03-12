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
            _connectionString = configuration.GetConnectionString("PantheonDB") ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<MedicalReport>> GetMedicalReportsForPatient(string patientId)
        {
            using var connection = new SqlConnection(_connectionString);
            
            var query = @"
                SELECT 
                    acNalaz, report.acSubject, adDate, acDescription, acDG, acTH, acKontrola, AcName2 as acLekar
                FROM _css_Nalaz report
                LEFT JOIN THE_SetSubj patient
                ON report.acLekar = patient.AcSubject
                WHERE report.acSubject = @AcSubject;
            ";

            var medicalReportsDbos = (await connection.QueryAsync<MedicalReportDbo>(query, new { AcSubject = patientId })).ToList();

            return _mapper.Map<IEnumerable<MedicalReport>>(medicalReportsDbos);
        }

        public async Task InsertMedicalReport(MedicalReport medicalReport)
        {
            var medicalReportDbo = _mapper.Map<MedicalReportDbo>(medicalReport);

            const string query = @"
            INSERT INTO _css_Nalaz (acNalaz, acSubject, adDate, acDescription, acDG, acTH, acKontrola, acLekar)
            VALUES (@AcNalaz, @AcSubject, @AdDate, @AcDescription, @AcDG, @AcTH, @AcKontrola, @AcLekar)";

            using var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.ExecuteAsync(query, medicalReportDbo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting medical report: {ex.Message}");
                throw;
            }
        }
    }
}
