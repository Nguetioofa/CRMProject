using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Models
{
    public class LogIdexViews
    {
        public int LogNo { get; set; }
        public int? employeeId { get; set; }
        public string LogLevel { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }
        public Customer Customer { get; set; } = new();

    }
}
