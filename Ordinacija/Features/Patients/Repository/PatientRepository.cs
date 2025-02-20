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
    }
}
