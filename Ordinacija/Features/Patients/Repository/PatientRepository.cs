using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Ordinacija.Features.Patients.Models;
using System.Data.SqlClient;

namespace Ordinacija.Features.Patients.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public PatientRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString("PantheonDB");
            _mapper = mapper;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT * FROM THE_SetSubj";
                    var patients = await connection.QueryAsync<PatientDbo>(query);
                    return _mapper.Map<List<Patient>>(patients);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw;
            }
        }

        public async Task InsertPatient(Patient patient)
        {
            var patientDbo = _mapper.Map<PatientDbo>(patient);

            var sql = @"
                INSERT INTO tHE_SetSubj (AcSubject, AcName2, AcAddress, AdBirthDate, AcFieldSA, AcFieldSC, AcFieldSD, AcFieldSE, AcFieldSF, AcFieldSG, AcFieldSH, AcFieldSI)
                VALUES (@AcSubject, @AcName2, @AcAddress, @AdDateOfBirth, @AcFieldSA, @AcFieldSC, @AcFieldSD, @AcFieldSE, @AcFieldSF, @AcFieldSG, @AcFieldSH, @AcFieldSI)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var lastId = await connection.ExecuteScalarAsync<string>("SELECT MAX(AcSubject) FROM tHE_SetSubj");
                int nextId = (string.IsNullOrEmpty(lastId) ? 1 : int.Parse(lastId) + 1);
                patientDbo.AcSubject = nextId.ToString("D6");

                await connection.ExecuteAsync(sql, patientDbo);
            }
        }

        public async Task UpdatePatient(Patient patient)
        {
            var sql = @"
                UPDATE tHE_SetSubj
                SET AcName2 = @AcName2, 
                    AcAddress = @AcAddress,
                    AdBirthDate = @AdBirthDate,
                    AcFieldSA = @AcFieldSA,
                    AcFieldSC = @AcFieldSC,
                    AcFieldSD = @AcFieldSD,
                    AcFieldSE = @AcFieldSE,
                    AcFieldSF = @AcFieldSF,
                    AcFieldSG = @AcFieldSG,
                    AcFieldSH = @AcFieldSH,
                    AcFieldSI = @AcFieldSI
                WHERE AcSubject = @AcSubject"
            ;

            try
            {
                var patientDbo = _mapper.Map<PatientDbo>(patient);

                using var connection = new SqlConnection(_connectionString);
                
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, patientDbo);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw;
            }
        }
    }
}
