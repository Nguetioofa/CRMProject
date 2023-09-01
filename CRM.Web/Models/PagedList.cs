using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Models
{
    public class PagedList<TModel> where TModel : class
    {
        public IEnumerable<TModel> ListElements { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
