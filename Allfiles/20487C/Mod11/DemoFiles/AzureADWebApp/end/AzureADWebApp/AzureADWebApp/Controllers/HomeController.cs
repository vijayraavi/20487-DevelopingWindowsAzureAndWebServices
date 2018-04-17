using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace AzureADWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            string userfirstName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.GivenName).Value;
            ViewBag.Message = String.Format("Welcome {0}!", userfirstName);

            return View();
        }
    }
}