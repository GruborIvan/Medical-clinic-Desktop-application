using Microsoft.Extensions.Configuration;
using Ordinacija.Features.Login.Models;
using System.Data.SqlClient;

namespace Ordinacija.Features.Login.Repository.Implementation
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;
        private readonly string _server;
        private readonly string _database;

        public LoginRepository(IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("PantheonDB")
                               ?? throw new ArgumentNullException(nameof(configuration));

            var builder = new SqlConnectionStringBuilder(connectionString);
            _server = builder.DataSource;
            _database = builder.InitialCatalog;
        }

        public async Task<bool> AuthenticateUser(LoginCredentials loginCredentials)
        {
            var dynamicConnectionString = $"Server={_server};Database={_database};User Id={loginCredentials.Username};Password={loginCredentials.Password};TrustServerCertificate=True;";

            try
            {
                using var connection = new SqlConnection(dynamicConnectionString);
                await connection.OpenAsync(); 
                return true; 
            }
            catch (SqlException)
            {
                return false; 
            }
        }
    }
}
