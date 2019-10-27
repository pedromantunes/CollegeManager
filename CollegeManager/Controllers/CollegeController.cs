using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeManager.Controllers
{
    public class CollegeController : Controller
    {
        // GET: College
        public ActionResult Index()
        {
            return View();
        }
    }
}