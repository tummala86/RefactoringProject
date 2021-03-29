using PinnacleSample.Database;
using PinnacleSample.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace PinnacleSample
{
    public class PartInvoiceRepositoryDB : IPartInvoiceRepository
    {
        private readonly SqlConnectionProvider _sqlConnectionProvider;

        public PartInvoiceRepositoryDB(SqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }

        public bool Add(PartInvoice invoice)
        {
            using (var connection = _sqlConnectionProvider.GetConnection())
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "PMS_AddPartInvoice"
                };

                SqlParameter stockCodeParameter = new SqlParameter("@StockCode", SqlDbType.VarChar, 50) { Value = invoice.StockCode };
                command.Parameters.Add(stockCodeParameter);
                SqlParameter quantityParameter = new SqlParameter("@Quantity", SqlDbType.Int) { Value = invoice.Quantity };
                command.Parameters.Add(quantityParameter);
                SqlParameter customerIdParameter = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = invoice.CustomerId };
                command.Parameters.Add(customerIdParameter);

                connection.Open();
                var recordCount = command.ExecuteNonQuery();
                return recordCount > 0;
            }
        }       
    }
}
