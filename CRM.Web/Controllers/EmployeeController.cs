using CRM.Web.Data.Contrat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM.Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using CRM.Web.Resources;

namespace CRM.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStringLocalizer<SharedLangResources> _localizer;

        public EmployeeController(IEmployeeRepository employeeRepository, IStringLocalizer<SharedLangResources> localizer)
        {
            _employeeRepository = employeeRepository;
            _localizer = localizer;

        }
        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeRepository.Authenticate(model);

                if (result.Resultat.Email == null)
                {
                    ViewBag.ErrorMessage = _localizer["Employee.failedconnection"];
                    return View(model);
                }

                var claimsList = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, result.Resultat.Email),
                        new Claim(ClaimTypes.Role, result.Resultat.Role),
                        new Claim("id", result.Resultat.EmployeeNo.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claimsList, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddHours(12),
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.ErrorMessage = _localizer["Employee.allfields"];
                return View(model);
            }
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

    }
}
