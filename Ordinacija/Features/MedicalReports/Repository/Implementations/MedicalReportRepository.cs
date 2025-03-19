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
                    acNalaz, report.acSubject, adDate, acDescription, acDG, acTH, acKontrola, acLekar, Anamneza, AcName2 as DoctorName
                FROM _css_Nalaz report
                LEFT JOIN THE_SetSubj patient
                ON report.acLekar = patient.AcSubject
                WHERE report.acSubject = @AcSubject
                ORDER BY adDate DESC;
            ";

            var medicalReportsDbos = (await connection.QueryAsync<MedicalReportDbo>(query, new { AcSubject = patientId })).ToList();

            return _mapper.Map<IEnumerable<MedicalReport>>(medicalReportsDbos);
        }

        public async Task InsertMedicalReport(MedicalReport medicalReport)
        {
            var medicalReportDbo = _mapper.Map<MedicalReportDbo>(medicalReport);
            medicalReportDbo.AcNalaz = await GenerateNextNalazId();

            const string query = @"
                INSERT INTO _css_Nalaz (acNalaz, acSubject, adDate, acDescription, acDG, acTH, acKontrola, acLekar, Anamneza)
                VALUES (@AcNalaz, @AcSubject, @AdDate, @AcDescription, @AcDG, @AcTH, @AcKontrola, @AcLekar, @Anamneza)
            ";

            using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(query, medicalReportDbo);
        }

        public async Task UpdateMedicalReport(MedicalReport medicalReport)
        {
            var medicalReportDbo = _mapper.Map<MedicalReportDbo>(medicalReport);

            const string query = @"
                UPDATE _css_Nalaz
                SET 
                    acSubject = @AcSubject,
                    adDate = @AdDate,
                    acDescription = @AcDescription,
                    acDG = @AcDG,
                    acTH = @AcTH,
                    acKontrola = @AcKontrola,
                    acLekar = @AcLekar,
                    Anamneza = @Anamneza
                WHERE acNalaz = @AcNalaz
            ";

            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(query, medicalReportDbo);
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        private async Task<string> GenerateNextNalazId()
        {
            const string sql = "SELECT MAX(acNalaz) FROM _css_Nalaz";

            using var connection = CreateConnection();
            var lastId = await connection.ExecuteScalarAsync<string>(sql);
            int nextId = string.IsNullOrEmpty(lastId) ? 1 : int.Parse(lastId) + 1;

            return nextId.ToString("D6");
        }
    }
}
