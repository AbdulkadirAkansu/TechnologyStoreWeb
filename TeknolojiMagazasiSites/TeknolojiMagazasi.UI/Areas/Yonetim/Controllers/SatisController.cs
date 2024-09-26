using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknolojiMagazasi.DataAccess;
using TeknolojiMagazasi.Model;
using TeknolojiMagazasi.Model.SepetClasses;
using TeknolojiMagazasi.UI.Filters;

namespace TeknolojiMagazasi.UI.Areas.Yonetim.Controllers
{
       [Kimlik, Yetki(Rol = Yetkiler.Mudur)]
        public class SatisController : Controller
        {
            // GET: Yonetim/Satis
            public ActionResult Listele()
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    return View(uow.SatısWork.GetAll());
                }
            }

            public ActionResult SatisDetay(int? id)
            {
                if (id != null)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                        return View(uow.SatısDetayWork.GetAllWithUrun((int)id));
                }
                return RedirectToAction("Listele");
            }

            public ActionResult Sil(int? id)
            {
                if (id != null)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        Satıs satıs = uow.SatısWork.GetItem(id);
                        if (satıs != null)
                        {
                            return View(satıs);
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                }
                return RedirectToAction("Listele");
            }

            [HttpPost, ValidateAntiForgeryToken, ActionName("Sil")]
            public ActionResult SilOnay(int? id, Satıs model)
            {
                if (id != null)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        Satıs satıs = uow.SatısWork.GetItem(id);
                        if (satıs != null)
                        {
                            uow.SatısWork.Remove(satıs);
                            uow.Save();
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                }
                return RedirectToAction("Listele");
            }

            [Yetki(Rol = Yetkiler.Kasiyer)]
            public ActionResult SatisEkle()
            {
                using (UnitOfWork uow = new UnitOfWork())
                    return View(uow.UrunWork.GetAllWithMarka());
            }

            [Yetki(Rol = Yetkiler.Kasiyer)]
            public ActionResult SepeteEkle(int? id)
            {
                if (id != null)
                {
                    Sepet sepet;
                    if (Session["sepet"] != null)
                        sepet = Session["sepet"] as Sepet;
                    else
                        sepet = new Sepet();

                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        Urun urun = uow.UrunWork.GetItem(id);
                        if (urun != null)
                        {
                            sepet.Ekle(urun);
                            Session["sepet"] = sepet;
                        }
                    }
                }

                return RedirectToAction("SatisEkle");
            }

            [Yetki(Rol = Yetkiler.Kasiyer)]
            public ActionResult SepeteGoruntule()
            {
                Sepet sepet;
                if (Session["sepet"] != null)
                    sepet = Session["sepet"] as Sepet;
                else
                    sepet = new Sepet();

                return View(sepet.Satıs);
            }

            [Yetki(Rol = Yetkiler.Kasiyer)]
            public ActionResult SatisTamamla()
            {
                Sepet sepet;
                if (Session["sepet"] != null)
                {
                    sepet = Session["sepet"] as Sepet;
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        sepet.Satıs.TarihSaat = DateTime.Now;
                        uow.SatısWork.Add(sepet.Satıs);

                        foreach (var item in sepet.Satıs.Detaylar)
                        {
                            Urun urun = uow.UrunWork.GetItem(item.Urun.Id);
                            urun.StokAdet -= item.Adet;
                            uow.UrunWork.Update(urun);
                        }

                        uow.Save();

                        //sepet boşalt
                        Session["sepet"] = null;
                    }
                }
                return RedirectToAction("SatisEkle");
            }
        }
}