using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Xml;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            var vm = new TestViewModel()
            {
                JustTime = DateTime.Now,
                JustDate = DateTime.Today.Subtract(TimeSpan.FromDays(478)),
                DateAndTime = DateTime.Now.AddMinutes(47856)
            };
            
            return View(vm);
        }


        [HttpPost]
        public IActionResult Test(TestViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                vm.JustTime = DateTime.Now.AddMinutes(60);
                vm.JustDate = DateTime.Today.Subtract(TimeSpan.FromDays(1));
                vm.DateAndTime = DateTime.Now.AddMinutes(60);
            }

            return View(vm);
        }

        
        // ================================== i18n ===================================
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                key: CookieRequestCultureProvider.DefaultCookieName,
                value: CookieRequestCultureProvider.MakeCookieValue(
                    requestCulture: new RequestCulture(culture: culture)),
                options: new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(years: 1)
                }
            );

            return LocalRedirect(localUrl: returnUrl);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}