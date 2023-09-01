namespace CRM.Web.Models
{
    public class CallReportModel
    {
        public int CallNo { get; set; }
        public string CompleteName { get; set; }
        public DateTime DateTimeofCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }

    }
}
