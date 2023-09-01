using CRM.Web.Data.Contrat;
using CRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Web.Data.DBAccess;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using static Dapper.SqlMapper;

namespace CRM.Web.Data.Implementation
{
    public class CustomerRepository : IGenericRepository<Customer>
    {
        private readonly DbConnection _dbconnection;
        ILogManager _logManager;
        public CustomerRepository(DbConnection dbconnection, ILogManager logManager)
        {
                _dbconnection = dbconnection;
            _logManager = logManager;
        }



        public async Task<Response<int>> CountAll()
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {

                    var nbCustomer = await connection.ExecuteScalarAsync<int>("CountAllCustomer",  CommandType.StoredProcedure);

                    return new Response<int>()
                    {
                        Resultat = nbCustomer,
                        IsCorrect = true
                    };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new();
            }
        }

        public async Task<Response<Customer>> GetAll()
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {

                    var customer = await connection.QueryAsync<Customer>("GetAllCustomers", commandType: CommandType.StoredProcedure);

                    return new Response<Customer>()
                    {
                        Resultats = customer,
                        IsCorrect = true
                    };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new();
            }
        }

        public async Task<Response<Customer>> GetById(int id)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {
                    var idcustomer = new { CustomerNo = id };
                    var customer = await connection.QuerySingleOrDefaultAsync<Customer>("GetCustomerById", idcustomer, commandType: CommandType.StoredProcedure);

                    return new Response<Customer>()
                    {
                        Resultat = customer,
                        IsCorrect = true
                    };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new();
            }
        }

        public async Task<Response<Customer>> GetPaged(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {
                    var pagedmodel = new { pageIndex, pageSize };

                    var customer = await connection.QueryAsync<Customer>("GetPagedCustomers", pagedmodel, commandType: CommandType.StoredProcedure);

                    return new Response<Customer>()
                    {
                        Resultats = customer,
                        IsCorrect = true
                    };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new();
            }
        }

        public async Task<Response<int>> Remove(int id)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {

                    var nocustomer = new { CustomerNo = id };

                    var result = await connection.ExecuteAsync("RemoveCustomer", nocustomer, commandType: CommandType.StoredProcedure);

                    return new Response<int>() { IsCorrect = result > 0, Resultat = result };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<int>() { IsCorrect = false, Resultat = 0 };
            }
        }

        public async Task<Response<int>> Update(Customer tModel)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {

                    var customerAdd = new
                    {
                        tModel.CustomerNo,
                        tModel.CustomerName,
                        tModel.CustomerSurname,
                        tModel.Address,
                        tModel.PostCode,
                        tModel.Country,
                        tModel.DateofBirth
                    };

                    var result = await connection.ExecuteAsync("UpdateCustomer", customerAdd, commandType: CommandType.StoredProcedure);

                    return new Response<int>() { IsCorrect = result > 0, Resultat = result };
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<int>() { IsCorrect = false, Resultat = 0 };
            }
        }

        public async Task<Response<int>> Add(Customer tModel)
        {
            try
            {
                using (var connection = _dbconnection.OpenConnection())
                {

                    var customerAdd = new
                    {
                        tModel.CustomerName,
                        tModel.CustomerSurname,
                        tModel.Address,
                        tModel.PostCode,
                        tModel.Country,
                        tModel.DateofBirth
                    };

                    var result = await connection.ExecuteAsync("AddCustomer", customerAdd, commandType: CommandType.StoredProcedure);

                    return new Response<int>() { IsCorrect = result > 0, Resultat = result} ;
                }
            }
            catch (Exception ex)
            {
                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return new Response<int>() { IsCorrect = false, Resultat = 0};
            }
        }

    }
}
