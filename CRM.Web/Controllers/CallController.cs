using CRM.Web.Data.Contrat;
using CRM.Web.Models;
using CRM.Web.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CRM.Web.Controllers
{
    [Authorize("Employee")]
    public class CallController : Controller
    {
        private readonly IGenericRepository<Call> _callRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly ILogManager _logManager;
        private readonly IStringLocalizer<SharedLangResources> _localization;

        public CallController(IGenericRepository<Call> callRepository, IGenericRepository<Customer> customerRepository, 
            ILogManager logManager, IStringLocalizer<SharedLangResources> localization)
        {
            _callRepository = callRepository;
            _customerRepository = customerRepository;
            _logManager = logManager;
            _localization = localization;
        }


        [HttpGet]
        public async Task<ActionResult> Index(int? customerNo, int page = 1, int pageSize = 10)
        {
            if (customerNo is null)
            {
                var response = await _callRepository.GetPaged(page, pageSize);
                if (response.Resultats is null)
                {
                    return NotFound();
                }
                var total = await _callRepository.CountAll();
                var totalPages = (int)Math.Ceiling((double)total.Resultat / pageSize);

                var callList = new List<CallViewsModel>();

                foreach (var item in response.Resultats)
                {
                    callList.Add(new CallViewsModel
                    {
                        CallNo = item.CallNo,
                        CustomerNo = item.CustomerNo,
                        customer = (await _customerRepository.GetById(item.CustomerNo)).Resultat,
                        Description = item.Description,
                        DateTimeofCall = item.DateTimeofCall,
                        Subject = item.Subject
                    });
                }

                var model = new PagedList<CallViewsModel>
                {
                    ListElements = callList,
                    pageSize = totalPages,
                    pageIndex = page,
                };

                return View(model);
            }
            else
            {
                var response = await _callRepository.GetAll();
                if (response.Resultats is null)
                {
                    return NotFound();
                }

                var list = response.Resultats.Where(c=> c.CustomerNo == customerNo);

                var total = list.Count();
                var totalPages = (int)Math.Ceiling((double)total / pageSize);

                var callList = new List<CallViewsModel>();

                foreach (var item in list)
                {
                    callList.Add(new CallViewsModel
                    {
                        CallNo = item.CallNo,
                        CustomerNo = item.CustomerNo,
                        customer = (await _customerRepository.GetById(item.CustomerNo)).Resultat,
                        Description = item.Description,
                        DateTimeofCall = item.DateTimeofCall,
                        Subject = item.Subject
                    });
                }

                var model = new PagedList<CallViewsModel>
                {
                    ListElements = callList,
                    pageSize = totalPages,
                    pageIndex = page,
                };

                return View(model);

            }
        }

        // GET: CallController



        // GET: CallController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var call = await _callRepository.GetById(id);
            if (call.Resultat == null)
                return NotFound();

            var customer = await _customerRepository.GetById(call.Resultat.CustomerNo);
            if (customer.Resultat == null)
                return NotFound();

            var callview = new CallViewsModel(call.Resultat, customer.Resultat);
            
            return View(callview);
        }

        // GET: CallController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Call call)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _callRepository.Add(call);
                    if (result.IsCorrect)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = _localization["Echec"];
                        return View(call);
                    }
                }

                ViewBag.ErrorMessage = _localization["Employee.allfields"];
                return View(call);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = _localization["Echec"];

                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return View(call);
            }
        }

        // GET: CallController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var call = await _callRepository.GetById(id);
            if (call.Resultat == null)
                return NotFound();

            return View(call.Resultat);
        }

        // POST: CallController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Call call)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _callRepository.Update(call);
                    if (result.IsCorrect)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = _localization["Echec"];
                        return View(call);
                    }
                }

                ViewBag.ErrorMessage = _localization["Employee.allfields"];
                return View(call);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = _localization["Echec"];

                _logManager.AddLog("Error", ex.Message, ex.ToString());
                return View(call);
            }
        }

        // GET: CallController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var call = await _callRepository.GetById(id);
            if (call.Resultat == null)
                return NotFound();

            var customer = await _customerRepository.GetById(call.Resultat.CustomerNo);
            if (customer.Resultat == null)
                return NotFound();

            var callview = new CallViewsModel(call.Resultat, customer.Resultat);

            return View(callview);
        }

        // POST: CallController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Deleted(int id)
        {
            var resu = await _callRepository.Remove(id);
            if (resu.IsCorrect)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ErrorMessage = _localization["Echec"];
            return NoContent();
        }
    }
}
