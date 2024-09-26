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
    public class UrunController : Controller
    {
   // GET: Yonetim/Urun
            public ActionResult Listele()
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    return View(uow.UrunWork.GetAllWithMarka());
                }
            }

            public ActionResult Ekle()
            {
                using (UnitOfWork uow = new UnitOfWork())
                    ViewBag.Markalar = new SelectList(uow.MarkaWork.GetAll(), "Id", "Ad");

                return View();
            }

            [HttpPost, ValidateAntiForgeryToken]
            public ActionResult Ekle(Urun urun)
            {
                if (ModelState.IsValid)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        uow.UrunWork.Add(urun);
                        uow.Save();
                        return RedirectToAction("Listele");
                    }
                }
                using (UnitOfWork uow = new UnitOfWork())
                    ViewBag.Markalar = new SelectList(uow.MarkaWork.GetAll(), "Id", "Ad");
                ModelState.AddModelError("", "Ekleme başarısız");
                return View(urun);
            }

            public ActionResult Duzenle(int? id)
            {
                if (id != null)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        Urun urun = uow.UrunWork.GetItem(id);
                        if (urun != null)
                        {
                            ViewBag.Markalar = new SelectList(uow.MarkaWork.GetAll(), "Id", "Ad");
                            return View(urun);
                        }
                        else
                            return HttpNotFound();
                    }
                }
                return RedirectToAction("Listele");
            }

            [HttpPost, ValidateAntiForgeryToken]
            public ActionResult Duzenle(int? id, Urun urun)
            {
                if (ModelState.IsValid)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        uow.UrunWork.Update(urun);
                        uow.Save();
                        return RedirectToAction("Listele");
                    }
                }
                using (UnitOfWork uow = new UnitOfWork())
                    ViewBag.Markalar = new SelectList(uow.MarkaWork.GetAll(), "Id", "Ad");
                ModelState.AddModelError("", "Düzenleme yapılamadı");
                return View(urun);
            }

            public ActionResult Sil(int? id)
            {
                if (id != null)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        Urun urun = uow.UrunWork.GetItem(id);
                        if (urun != null)
                            return View(urun);
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
                        Urun urun = uow.UrunWork.GetItem(id);
                        if (urun != null)
                        {
                            uow.UrunWork.Remove(urun);
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