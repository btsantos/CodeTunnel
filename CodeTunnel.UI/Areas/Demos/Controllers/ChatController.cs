using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignalR.Hubs;

namespace CodeTunnel.UI.Areas.Demos.Controllers
{
    public class ChatController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nickname)
        {
            ViewBag.Nickname = nickname;
            return View();
        }
    }

    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            Clients.addMessage(string.Format("<div>{0}: {1}</div>", Caller.nickname, message));
        }

        public void Enter()
        {
            Clients.addMessage(string.Format("<div>-= {0} has entered the room =-</div>", Caller.nickname));
        }
    }
}
