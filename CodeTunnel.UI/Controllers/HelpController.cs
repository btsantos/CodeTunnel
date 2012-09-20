using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeTunnel.UI.Controllers
{
    /// <summary>
    /// Controller to handle various help related functions.
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// Display the help home screen.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Display Markdown syntax help.
        /// </summary>
        public ViewResult Markdown()
        {
            return View();
        }
    }
}
