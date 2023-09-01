namespace CRM.Web.Models
{
    public class ReportIndexModel
    {
        public IEnumerable<CallReportModel> listCalls { get; set; }
        public IEnumerable<Customer> listCustomers { get; set; }
    }
}
