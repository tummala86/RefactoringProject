using PinnacleSample.Database;
using System.Data;
using System.Data.SqlClient;

namespace PinnacleSample
{
    public class CustomerRepositoryDB : ICustomerRepository
    {
        private readonly SqlConnectionProvider _sqlConnectionProvider;

        public CustomerRepositoryDB(SqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        public Customer GetByName(string name)
        {
            Customer customer = null;

            using (var connection = _sqlConnectionProvider.GetConnection())
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "CRM_GetCustomerByName"
                };

                SqlParameter parameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
                command.Parameters.Add(parameter);

                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    customer = new Customer
                    {
                        Id = int.Parse(dataReader["CustomerID"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Address = dataReader["Address"].ToString()
                    };
                }
            }

            return customer;
        }
    }
}
