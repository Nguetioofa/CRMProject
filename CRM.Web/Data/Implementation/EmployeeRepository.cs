using CRM.Web.Data.Contrat;
using CRM.Web.Data.DBAccess;
using CRM.Web.Models;
using Dapper;
using System.Data;

namespace CRM.Web.Data.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbConnection _dbConnection;

        public EmployeeRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Response<Employee>> Authenticate(LoginModel login)
        {
            try
            {
                using (var connection = _dbConnection.OpenConnection())
                {

                    var result = await connection.QueryFirstOrDefaultAsync<Employee>("Login", login, commandType: CommandType.StoredProcedure);

                    return new Response<Employee>() 
                    {
                        IsCorrect = true,
                        Resultat = result,
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new();
            }
        }
    
    }
}
