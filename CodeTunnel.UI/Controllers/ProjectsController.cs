using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json.Linq;
using CodeTunnel.UI.Models;

namespace CodeTunnel.UI.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult Index()
        {
            var json = new WebClient().DownloadString("https://api.github.com/users/chevex/repos");
            var jRepositories = JArray.Parse(json);
            var repositories = new List<Repository>();
            foreach (var jRepository in jRepositories)
                repositories.Add(new Repository(jRepository.ToString()));
            return View(repositories.OrderByDescending(x => x.LastUpdated));
        }
    }
}
