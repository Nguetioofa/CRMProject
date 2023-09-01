using CRM.Web.Data.Contrat;
using CRM.Web.Data.Implementation;
using CRM.Web.Models;
using CRM.Web.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using FastReport;
using FastReport.Export.PdfSimple;


namespace CRM.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportRepository _reportRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly ILogManager _logManager;
        private readonly IStringLocalizer<SharedLangResources> _localization;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public ReportController(IWebHostEnvironment hostingEnvironment, ReportRepository reportRepository, IGenericRepository<Customer> customerRepository,
            ILogManager logManager, IStringLocalizer<SharedLangResources> localization)
        {
            _reportRepository = reportRepository;
            _customerRepository = customerRepository;
            _logManager = logManager;
            _localization = localization;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: ReportController
        public async Task<IActionResult> Index()
        {
            var listModelReport = new ReportIndexModel();
            
            listModelReport.listCalls = await _reportRepository.GetCallsWithCompleteName();
            listModelReport.listCustomers = (await _customerRepository.GetAll()).Resultats;
            
            return View(listModelReport);
        }

        public async Task<FileResult> Print()
        {
            FastReport.Utils.Config.WebMode = true;
            Report report = new Report();
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            string reportFilePath = Path.Combine(wwwrootPath, "Resources", "Report", "CRMReport.frx");

            report.Load(reportFilePath);

            IEnumerable<CallReportModel>? listCalls = await _reportRepository.GetCallsWithCompleteName();
            IEnumerable<Customer>? listCustomers = (await _customerRepository.GetAll()).Resultats;

            report.RegisterData(listCustomers, "listCustomersRef");
            report.RegisterData(listCalls, "listCallsRef");
            if (report.Report.Prepare())
            {
                PDFSimpleExport pdf = new PDFSimpleExport();
                pdf.ShowProgress = false;
                pdf.Subject = "Report";
                pdf.Title = "Report";
                var ms = new MemoryStream();
                report.Report.Export(pdf, ms);
                report.Dispose();
                pdf.Dispose();
                ms.Position = 0;
                Guid guid = Guid.NewGuid();
                
                return File(ms.ToArray(), "application/pdf", $"{guid.ToString("D")}.pdf");
            }
            else
            {
                return null;
            }
        }
    }
}
