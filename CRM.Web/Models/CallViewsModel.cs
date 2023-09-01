using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Models
{
    public class CallViewsModel
    {
        public int CallNo { get; set; }
        public int CustomerNo { get; set; }
        public DateTime DateTimeofCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Customer customer { get; set; } = new();
        public DateTime RegistrationDate { get; set; }

        public CallViewsModel(Call call,Customer customer1)
        {
            CallNo = call.CallNo;
            CustomerNo = call.CustomerNo;
            DateTimeofCall = call.DateTimeofCall;
            Subject = call.Subject;
            Description = call.Description;
            customer = customer1;
            RegistrationDate = DateTime.Now;
        }

        public CallViewsModel() 
        {
            CallNo = 0;
            CustomerNo = 0;
            DateTimeofCall = DateTime.Now;
            Subject = string.Empty;
            Description = string.Empty;
            customer = new();
            RegistrationDate = DateTime.Now;
        }
    }
}
