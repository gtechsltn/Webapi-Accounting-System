using Dapper;
using Npgsql;
using System.Data;

namespace AccountingWebAPI.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateDbConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            //connection.ExecuteAsync("SET search_path TO public");

            return connection;
        }
    }
}
