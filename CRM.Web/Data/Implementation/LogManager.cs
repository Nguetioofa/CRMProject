using CRM.Web.Data.Contrat;
using CRM.Web.Data.DBAccess;
using CRM.Web.Models;
using System.Data;
using Dapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Web.Data.Implementation
{
    public class LogManager : ILogManager
    {
        private readonly DbConnection _dbConnection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogManager(DbConnection dbConnection, IHttpContextAccessor httpContextAccessor)
        {
            _dbConnection = dbConnection;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task AddLog(string logLevel, string message, string exception)
        {


            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("id");
                int userId = 0;
            if (userIdClaim != null)
            {
                 userId = Convert.ToInt32(userIdClaim.Value);
            }
            Log log = new Log(userId, logLevel, message, exception);

            try
            {
                using (var connection = _dbConnection.OpenConnection())
                {
                     await connection.QueryFirstOrDefaultAsync("AddLog", log, commandType: CommandType.StoredProcedure);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
