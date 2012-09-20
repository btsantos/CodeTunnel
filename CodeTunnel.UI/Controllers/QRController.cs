using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeTunnel.Domain.Interfaces;

namespace CodeTunnel.UI.Controllers
{
    public class QRController : Controller
    {
        private IVariableRepository _variableRepository;

        public QRController(IVariableRepository variableRepository)
        {
            this._variableRepository = variableRepository;
        }

        public string Index()
        {
            return _variableRepository.GetVariable("QR").Value;
        }

        public JsonResult Update(string value)
        {
            _variableRepository.GetVariable("QR").Value = value;
            _variableRepository.SaveChanges();
            return Json(new { status = "QR data updated successfully." }, JsonRequestBehavior.AllowGet);
        }
    }
}
