using System.Web.Mvc;

namespace CodeTunnel.UI.Areas.Demos
{
    public class DemosAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Demos";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                null,
                "Demos/CoolType/{text}",
                new { controller="CoolType", action = "Index", text = UrlParameter.Optional }
            );
            context.MapRoute(
                null,
                "Demos/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
