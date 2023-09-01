using CRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Data.Contrat
{
    public interface ILogManager
    {
        Task AddLog(string logLevel, string message,string exception);

    }
}
