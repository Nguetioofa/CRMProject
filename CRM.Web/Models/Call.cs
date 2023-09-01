using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Models
{
    public class Call
    {
        public int CallNo { get; set; }
        public int CustomerNo { get; set; }
        public DateTime DateTimeofCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public Call(CallViewsModel call)
        {
            CallNo = call.CallNo;
            CustomerNo = call.CustomerNo;
            DateTimeofCall = call.DateTimeofCall;
            Subject = call.Subject;
            Description = call.Description;
        }

        public Call()
        {
            CallNo = 0;
            CustomerNo = 0;
            DateTimeofCall = DateTime.Now;
            Subject = string.Empty;
            Description = string.Empty;
        }
    }

}
