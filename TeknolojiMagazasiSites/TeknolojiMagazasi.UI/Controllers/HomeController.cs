using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknolojiMagazasi.DataAccess;

namespace TeknolojiMagazasi.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                uow.UrunWork.GetAll();
            }

            return View();
        }

        public ActionResult Hakkimizda()
        {         
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
    }
}