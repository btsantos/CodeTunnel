using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeTunnel.MvcUtilities.Components;

namespace CodeTunnel.UI.Controllers
{
    /// <summary>
    /// Base controller for CodeTunnel to perform common functions between all controllers.
    /// </summary>
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class BaseController : AjaxHeaderPreservationController
    {
    }
}