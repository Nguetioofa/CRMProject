using CRM.Web.Data.Contrat;
using CRM.Web.Data.DBAccess;
using CRM.Web.Data.Implementation;
using CRM.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;

namespace CRM.Web.MainExtension
{
    public static class CustomServicesContainer
    {
        public static IServiceCollection AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<DbConnection>();
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<IGenericRepository<Customer>, CustomerRepository>();
            builder.Services.AddTransient<IGenericRepository<Call>, CallRepository>();
            builder.Services.AddTransient<ReportRepository>();
            builder.Services.AddTransient<ILogManager, LogManager>();

            builder.Services.AddControllersWithViews()
                     .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("fr")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(12);
                    options.LoginPath = "/Employee/Login";
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Employee", policy =>
                    policy.RequireRole("Employee"));
                options.AddPolicy("Manager", policy =>
                policy.RequireRole("Manager"));
            });

            return builder.Services;
        }
    }
}
