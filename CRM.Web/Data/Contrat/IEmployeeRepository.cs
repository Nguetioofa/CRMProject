using CRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Data.Contrat
{
    public interface IEmployeeRepository
    {
        Task<Response<Employee>> Authenticate(LoginModel login);

    }
}
