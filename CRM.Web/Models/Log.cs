using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Models
{
    public class Log
    {
        
        public int EmployeeId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }

        public Log(int employeeId, string logLevel,string message,string exception)
        {
            EmployeeId = employeeId;
            LogLevel = logLevel;
            Message = message;
            Exception = exception;
        }

    }
}
