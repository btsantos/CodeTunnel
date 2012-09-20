using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Xml.Linq;

namespace CodeTunnel.UI.Controllers
{
    public class RSSController : Controller
    {
        public RedirectResult HuffAndStapes()
        {
            return RedirectPermanent("http://www.toadhopnetwork.com/podcast?categoryID2=321");
        }
    }
}
