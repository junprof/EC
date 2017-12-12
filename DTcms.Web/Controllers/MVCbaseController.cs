using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTcms.Web.Controllers
{
    public class MVCbaseController : Controller
    {
        // GET: Electricity
        public ActionResult V(string id)
        {
            return View(id);
        }
    }
}