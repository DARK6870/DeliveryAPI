using Microsoft.Data.SqlClient;
using System.Data;

namespace TEST.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConn");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }

}
