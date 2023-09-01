using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Models
{
    public class CustomerViewsModel
    {
        public CustomerViewsModel(Customer customer)
        {
            CustomerNo = customer.CustomerNo;
            CustomerName = customer.CustomerName;
            CustomerSurname = customer.CustomerSurname;
            Address = customer.Address;
            PostCode = customer.PostCode;
            Country = customer.Country;
            DateofBirth = customer.DateofBirth;
        }

        public CustomerViewsModel()
        {
            CustomerNo = 0;
            CustomerName = string.Empty;
            CustomerSurname = string.Empty;
            Address = string.Empty;
            PostCode = string.Empty;
            Country = string.Empty;
            DateofBirth = DateTime.Today;
        }

        public int CustomerNo { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public DateTime DateofBirth { get; set; }
    }

}
