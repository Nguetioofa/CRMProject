using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Web.Models;

namespace CRM.Web.Data.Contrat
{
    public interface IGenericRepository<TModel> where TModel : class
    {

        Task<Response<int>> Add(TModel tModel);
        Task<Response<int>> Update(TModel tModel);
        Task<Response<int>> Remove(int id);
        Task<Response<TModel>> GetAll();
        Task<Response<TModel>> GetPaged(int pageIndex = 1, int pageSize = 10);
        Task<Response<TModel>> GetById(int id);
        Task<Response<int>> CountAll();
    }
}
