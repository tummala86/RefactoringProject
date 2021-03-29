using System.Data.SqlClient;

namespace PinnacleSample.Database
{
    public class SqlConnectionProvider
    {
        private readonly string _connectionString;

        public SqlConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
