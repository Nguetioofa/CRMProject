using CRM.Web.Data.Contrat;
using CRM.Web.Models;
using Dapper;
using CRM.Web.Data.DBAccess;
using System.Data;

namespace CRM.Web.Data.Implementation
{
    public class ReportRepository
    {
        private readonly DbConnection _dbconnection;
        private readonly ILogManager _logManager;

        public ReportRepository(DbConnection dbConnection, ILogManager logManager)
        {
            _dbconnection = dbConnection;
            _logManager = logManager;
        }

        public async Task<IEnumerable<CallReportModel>> GetCallsWithCompleteName()
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {

                    var calls = await connection.QueryAsync<CallReportModel>("GetCallsWithCompleteName", commandType: CommandType.StoredProcedure);

                    return calls;

                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return Enumerable.Empty<CallReportModel>(); 
            }
        }
    }
}
