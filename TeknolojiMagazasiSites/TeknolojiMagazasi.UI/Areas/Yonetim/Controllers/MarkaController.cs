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
    [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
    public class MarkaController : Controller
    {
        // GET: Yonetim/Marka
        public ActionResult Listele()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.MarkaWork.GetAll());
            }
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(Marka marka)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.MarkaWork.Add(marka);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Ekleme başarısız");
            return View(marka);
        }

        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Marka marka = uow.MarkaWork.GetItem(id);
                    if (marka != null)
                        return View(marka);
                    else
                        return HttpNotFound();
                }
            }
            return RedirectToAction("Listele");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(int? id, Marka marka)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.MarkaWork.Update(marka);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Düzenleme yapılamadı");
            return View(marka);
        }

        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Marka marka = uow.MarkaWork.GetItem(id);
                    if (marka != null)
                        return View(marka);
                    else
                        return HttpNotFound();
                }
            }
            return RedirectToAction("Listele");
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Sil")]
        public ActionResult SilOnay(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Marka marka = uow.MarkaWork.GetItem(id);
                    if (marka != null)
                    {
                        uow.MarkaWork.Remove(marka);
                        uow.Save();
                        return RedirectToAction("Listele");
                    }
                    else
                        return HttpNotFound();
                }
            }
            return RedirectToAction("Listele");
        }
    }
}