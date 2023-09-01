using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRM.Web.Data.Contrat;
using CRM.Web.Data.DBAccess;
using CRM.Web.Models;
using Dapper;

namespace CRM.Web.Data.Implementation
{
    public class CallRepository : IGenericRepository<Call>
    {
        private readonly DbConnection _dbconnection;
        private readonly ILogManager _logManager;

        public CallRepository(DbConnection dbconnection, ILogManager logManager)
        {
            _dbconnection = dbconnection;
            _logManager = logManager;
        }

        public async Task<Response<int>> Add(Call tModel)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {
                    var callAdd = new
                    {
                        tModel.CustomerNo,
                        tModel.DateTimeofCall,
                        tModel.Subject,
                        tModel.Description
                    };

                    var result = await connection.ExecuteAsync("AddCall", callAdd, commandType: CommandType.StoredProcedure);

                    return new Response<int>() { IsCorrect = result > 0, Resultat = result };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<int>() { IsCorrect = false, Resultat = 0 };
            }

        }

        public async Task<Response<int>> CountAll()
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {
                    var nbCalls = await connection.ExecuteScalarAsync<int>("CountAllCalls", CommandType.StoredProcedure);

                    return new Response<int>() { Resultat = nbCalls, IsCorrect = true };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<int>() { IsCorrect = false };
            }
        }

        public async Task<Response<Call>> GetAll()
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {
                    var calls = await connection.QueryAsync<Call>("GetAllCalls", commandType: CommandType.StoredProcedure);

                    return new Response<Call>() { Resultats = calls, IsCorrect = true };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<Call>() { IsCorrect = false };
            }
        }

        public async Task<Response<int>> Update(Call tModel)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {

                    var result = await connection.ExecuteAsync("UpdateCall", tModel, commandType: CommandType.StoredProcedure);

                    return new Response<int>() { IsCorrect = result > 0, Resultat = result };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<int>() { IsCorrect = false, Resultat = 0 };
            }
        }

        public async Task<Response<int>> Remove(int id)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {
                    var nocall = new { CallNo = id };

                    var result = await connection.ExecuteAsync("RemoveCall", nocall, commandType: CommandType.StoredProcedure);

                    return new Response<int>() { IsCorrect = result > 0, Resultat = result };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<int>() { IsCorrect = false, Resultat = 0 };
            }
        }

        public async Task<Response<Call>> GetById(int id)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {
                    var idCall = new { CallNo = id };
                    var call = await connection.QuerySingleOrDefaultAsync<Call>("GetCallById", idCall, commandType: CommandType.StoredProcedure);

                    return new Response<Call>() { Resultat = call, IsCorrect = true };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<Call>() { IsCorrect = false };
            }
        }

        public async Task<Response<Call>> GetPaged(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {
                    var pagedModel = new { pageIndex, pageSize };

                    var calls = await connection.QueryAsync<Call>("GetPagedCalls", pagedModel, commandType: CommandType.StoredProcedure);

                    return new Response<Call>() { Resultats = calls, IsCorrect = true };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<Call>() { IsCorrect = false };
            }
        }



    }
}
