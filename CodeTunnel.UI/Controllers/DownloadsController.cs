using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace CodeTunnel.UI.Controllers
{
    /// <summary>
    /// Controller to handle functions related to downloading & uploading files.
    /// </summary>
    public class DownloadsController : Controller
    {
        /// <summary>
        /// Display a list of available downloads.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Show a form that allows the logged in user to browse files on the server and upload new files.
        /// </summary>
        [HttpGet]
        [Authorize]
        public ActionResult FileBrowser()
        {
            return View();
        }

        /// <summary>
        /// Allows the logged in user to upload a file to the server.
        /// </summary>
        [HttpPost]
        [Authorize]
        public JsonResult Upload(int? chunk, string name, string directory)
        {
            var fileUpload = Request.Files[0];
            var uploadPath = Server.MapPath("~/Content/");
            chunk = chunk ?? 0;
            using (var fs = new FileStream(Path.Combine(uploadPath, name), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }
            return Json(new { success = true });
        }
    }
}
