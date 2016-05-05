using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mod03.Controllers
{
    public class SamplePOCOController
    {
        [ActionContext]
        public ActionContext ActionContext { get; set; }

        [ViewDataDictionary]
        public ViewDataDictionary ViewData { get; set; }

        public IActionResult Index()
        {
            ViewData.Add("Message", "Hello from POCO Controller - Home page");
            ViewData.Add("Content", "This text was generated on server and returned from POCO controller at " + DateTime.Now.ToLocalTime() + " server time.");

            return new ViewResult() { ViewData = ViewData };
        }

        public IActionResult About()
        {
            ViewData.Add("Message", "Hello from POCO Controller - About page");
            ViewData.Add("Content", "This text was generated on server and returned from POCO controller at " + DateTime.Now.ToLocalTime() + " server time.");

            return new ViewResult() { ViewData = ViewData };
        }

        public IActionResult Contact()
        {
            ViewData.Add("Message", "Hello from POCO Controller - Contact page");
            ViewData.Add("Content", "This text was generated on server and returned from POCO controller at " + DateTime.Now.ToLocalTime() + " server time.");

            return new ViewResult() { ViewData = ViewData };
        }
    }
}
