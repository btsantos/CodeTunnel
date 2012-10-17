using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeTunnel.UI
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Session.SessionID;
            result.InnerHtml = Session["oldDate"].ToString();
            var newDate = DateTime.Now;
            result.InnerHtml += "<br />" + newDate.ToString();
            Session["oldDate"] = newDate;
        }
    }
}