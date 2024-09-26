using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknolojiMagazasi.DataAccess;
using TeknolojiMagazasi.Model;
using TeknolojiMagazasi.UI.Filters;

namespace TeknolojiMagazasi.UI.Areas.Yonetim.Controllers
{
    public class KullaniciController : Controller
    {
        [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
        public ActionResult Listele()
        {
            using (UnitOfWork uow = new UnitOfWork())
                return View(uow.KullanıcıWork.GetAll());
        }

        [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
        public ActionResult Ekle(Kullanıcı kullanıcı)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KullanıcıWork.Add(kullanıcı);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            return View(kullanıcı);
        }

        [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
        public ActionResult Duzenle(string eposta)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Kullanıcı kullanıcı = uow.KullanıcıWork.GetItem(eposta);
                if (kullanıcı != null)
                    return View(kullanıcı);
                else
                    return HttpNotFound();
            }
        }

        [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kullanıcı kullanıcı)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KullanıcıWork.Update(kullanıcı);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            return View(kullanıcı);
        }

        [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
        public ActionResult Sil(string eposta)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Kullanıcı kullanıcı = uow.KullanıcıWork.GetItem(eposta);
                if (kullanıcı != null)
                    return View(kullanıcı);
                else
                    return HttpNotFound();
            }
        }

        [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
        [HttpPost, ValidateAntiForgeryToken, ActionName("Sil")]
        public ActionResult SilOnay(string eposta, Kullanıcı model)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Kullanıcı kullanıcı = uow.KullanıcıWork.GetItem(model.EPosta);
                if (kullanıcı != null)
                {
                    uow.KullanıcıWork.Remove(kullanıcı);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
                else
                    return HttpNotFound();
            }
        }


        public ActionResult Login()
        {
            if (Session["user"] == null)
                return View();
            else
            {
                return RedirectToAction("Index", "DashBoard");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(Kullanıcı kullanıcı)
        {
            if (kullanıcı != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (uow.KullanıcıWork.Login(kullanıcı.EPosta, kullanıcı.Parola))
                    {
                        Kullanıcı user = uow.KullanıcıWork.GetItem(kullanıcı.EPosta);
                        Session["user"] = user;
                        return RedirectToAction("Index", "DashBoard");
                    }
                }
            }
            ModelState.AddModelError("", "Kullanıcı adı ya da parola hatalı!!!");
            return View(kullanıcı);
        }

        public ActionResult Logout()
        {
            if (Session["user"] != null)
                Session.Remove("user");
            return RedirectToAction("Login");
        }
    }
}