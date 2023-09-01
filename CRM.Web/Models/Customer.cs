using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Models
{
    public class Customer
    {
        public int CustomerNo { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Customer()
        {
            CustomerNo = 0;
            CustomerName = string.Empty;
            CustomerSurname = string.Empty;
            Address = string.Empty;
            PostCode = string.Empty;
            Country = string.Empty;
            DateofBirth = DateTime.Now;
            DateofBirth = DateTime.Now;
        }

        public Customer(CustomerViewsModel model)
        {
            CustomerNo = model.CustomerNo;
            CustomerName = model.CustomerName;
            CustomerSurname = model.CustomerSurname;
            Address = model.Address;
            PostCode = model.PostCode;
            Country = model.Country;
            DateofBirth = model.DateofBirth;
            DateofBirth = DateTime.Now;
        }
    }

}
