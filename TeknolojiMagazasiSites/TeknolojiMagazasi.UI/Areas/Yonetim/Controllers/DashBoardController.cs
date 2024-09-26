using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknolojiMagazasi.UI.Filters;

namespace TeknolojiMagazasi.UI.Areas.Yonetim.Controllers
{
    [Kimlik]
    public class DashBoardController : Controller
    {
        // GET: Yonetim/DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}