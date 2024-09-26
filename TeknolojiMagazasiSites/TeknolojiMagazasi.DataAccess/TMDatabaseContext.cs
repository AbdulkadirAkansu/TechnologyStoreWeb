using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasi.Model;

namespace TeknolojiMagazasi.DataAccess
{
    public class TMDatabaseContext : DbContext
    {
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Kullanıcı> Kullanıcılar { get; set; }
        public DbSet<Satıs> Satıslar { get; set; }
        public DbSet<SatısDetay> SatısDetaylar { get; set; }

        public TMDatabaseContext() : base("TeknolojiMagazasiSitesDB")
        {
            Database.SetInitializer(new MyInitializer());
        }
    }

    public class MyInitializer : CreateDatabaseIfNotExists<TMDatabaseContext>
    {
        protected override void Seed(TMDatabaseContext context)
        {
            context.Set<Kullanıcı>().Add(new Kullanıcı { EPosta = "user1@gmail.com", Ad = "User1", Soyad = "User1", Parola = "1234", Yetki = Yetkiler.Mudur });
            context.Set<Kullanıcı>().Add(new Kullanıcı { EPosta = "user2@gmail.com", Ad = "User2", Soyad = "User2", Parola = "1234", Yetki = Yetkiler.Kasiyer });

            Marka marka1 = context.Set<Marka>().Add(new Marka { Ad = "Marka 1" });
            Marka marka2 = context.Set<Marka>().Add(new Marka { Ad = "Marka 2" });
            Marka marka3 = context.Set<Marka>().Add(new Marka { Ad = "Marka 3" });

            context.SaveChanges();

            Urun urun1 = context.Set<Urun>().Add(new Urun { Ad = "Ürün 1", Marka = marka1, StokAdet = 100, Fiyat = 5 });
            Urun urun2 = context.Set<Urun>().Add(new Urun { Ad = "Ürün 2", Marka = marka2, StokAdet = 100, Fiyat = 2 });
            Urun urun3 = context.Set<Urun>().Add(new Urun { Ad = "Ürün 3", Marka = marka3, StokAdet = 100, Fiyat = 3 });
            Urun urun4 = context.Set<Urun>().Add(new Urun { Ad = "Ürün 4", Marka = marka1, StokAdet = 100, Fiyat = 1 });

            Satıs satıs1 = context.Set<Satıs>().Add(new Satıs { TarihSaat = DateTime.Now, ToplamTutar = 0 });
            Satıs satıs2 = context.Set<Satıs>().Add(new Satıs { TarihSaat = DateTime.Now, ToplamTutar = 0 });
            Satıs satıs3 = context.Set<Satıs>().Add(new Satıs { TarihSaat = DateTime.Now, ToplamTutar = 0 });

            satıs1.Detaylar = new List<SatısDetay>()
            {
                new SatısDetay{Urun = urun1, Adet = 2, Tutar = urun1.Fiyat * 2},
                new SatısDetay{Urun = urun2, Adet = 1, Tutar = urun2.Fiyat * 1},
                new SatısDetay{Urun = urun3, Adet = 5, Tutar = urun3.Fiyat * 5},
            };

            satıs2.Detaylar = new List<SatısDetay>()
            {
                new SatısDetay{Urun = urun3, Adet = 3, Tutar = urun3.Fiyat * 3},
                new SatısDetay{Urun = urun1, Adet = 5, Tutar = urun1.Fiyat * 5},
                new SatısDetay{Urun = urun4, Adet = 2, Tutar = urun4.Fiyat * 2},
            };

            satıs3.Detaylar = new List<SatısDetay>()
            {
                new SatısDetay{Urun = urun4, Adet = 3, Tutar = urun4.Fiyat * 3},
                new SatısDetay{Urun = urun1, Adet = 1, Tutar = urun1.Fiyat * 1},
                new SatısDetay{Urun = urun2, Adet = 4, Tutar = urun3.Fiyat * 4},
            };

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
