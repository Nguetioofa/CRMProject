using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Web.Models
{
    public class Response<T>
    {
        public T? Resultat { get; set; }
        public bool IsCorrect { get; set; } = false;
        public string? Message { get; set; }
        public IEnumerable<T>? Resultats { get; set; }
    }
}
