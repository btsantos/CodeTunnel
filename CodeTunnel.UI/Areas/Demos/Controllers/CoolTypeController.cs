using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeTunnel.UI.Areas.Demos.ViewModels;

namespace CodeTunnel.UI.Areas.Demos.Controllers
{
    public class CoolTypeController : Controller
    {
        public ActionResult Index(string text)
        {
            CoolTypeViewModel viewModel = new CoolTypeViewModel
            {
                Text = text
            };
            return View(viewModel);
        }
    }
}
