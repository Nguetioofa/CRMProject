using CRM.Web.Data.Contrat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM.Web.Models;
using Microsoft.Extensions.Localization;
using CRM.Web.Resources;

namespace CRM.Web.Controllers
{
    [Authorize("Manager")]
    public class CustomerController : Controller
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly ILogManager _logManager;
        private readonly IStringLocalizer<SharedLangResources> _localization;

        public CustomerController(IGenericRepository<Customer> customerRepository,ILogManager logManager, IStringLocalizer<SharedLangResources> localization)
        {
            _customerRepository = customerRepository;
            _logManager = logManager;
            _localization = localization;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var response = await _customerRepository.GetPaged(page, pageSize);
            if (response.Resultats is null)
            {
                return NotFound();
            }
            var total = await _customerRepository.CountAll();
            var totalPages = (int)Math.Ceiling((double)total.Resultat / pageSize);

            var model = new PagedList<Customer>
            {
                ListElements = response.Resultats,
                pageSize = totalPages,
                pageIndex = page,
            };

            return View(model);
        }


        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer.Resultat == null)
                return NotFound();

            return View(customer.Resultat);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerViewsModel customerViewsModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _customerRepository.Add(new Customer(customerViewsModel));
                    if (result.IsCorrect)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = _localization["Echec"];
                        return View(customerViewsModel);
                    }
                }

                ViewBag.ErrorMessage = _localization["Employee.allfields"];
                return View(customerViewsModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = _localization["Echec"];

                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return View(customerViewsModel);
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var customer = (await _customerRepository.GetById(id));
            if (customer.Resultat == null)
                return NotFound();
            var model = new CustomerViewsModel(customer.Resultat);
            return View(model);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerViewsModel customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _customerRepository.Update(new Customer(customer));
                    if (result.IsCorrect)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = _localization["Echec"];
                        return View(customer);
                    }
                }

                ViewBag.ErrorMessage = _localization["Employee.allfields"];
                return View(customer);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = _localization["Echec"];

                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return View(customer);
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var customer = (await _customerRepository.GetById(id));
            if (customer.Resultat == null)
                return NotFound();

            return View(customer.Resultat);
        }

        // POST: CustomerController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Deleted(int id)
        {
            var resu = await _customerRepository.Remove(id);
            if (resu.IsCorrect)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ErrorMessage = _localization["Echec"];
            return NoContent();
        }
    }
}
