using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CRM.Web.Data.DBAccess
{

    public class DbConnection
    {
        private readonly string connectionString;

        public DbConnection(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("SQLServerConnection");
        }

        public SqlConnection OpenConnection()
        {
            var sqlConnection = new SqlConnection(connectionString);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            return sqlConnection;
        }
    }
}
