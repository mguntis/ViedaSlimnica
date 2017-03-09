using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViedaSlimnicaProject.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Pacients");
        }
        public ActionResult Http404()
        {
            return View();
        }
        public ActionResult Http500()
        {
            return View();
        }
        public ActionResult General()
        {
            return RedirectToAction("Error", "Shared");
        }
    }
}