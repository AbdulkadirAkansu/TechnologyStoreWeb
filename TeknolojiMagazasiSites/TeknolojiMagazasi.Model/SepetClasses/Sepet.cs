using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknolojiMagazasi.Model.SepetClasses
{
    public class Sepet
    {
        public Satıs Satıs { get; set; }

        public Sepet()
        {
            Satıs = new Satıs();
        }

        public void Ekle(Urun urun)
        {
            if (Satıs.Detaylar.FirstOrDefault(x => x.Urun.Id == urun.Id) == null)
            {
                Satıs.Detaylar.Add(new SatısDetay { Adet = 1, Urun = urun, Tutar = urun.Fiyat });
                Satıs.ToplamTutar += urun.Fiyat;
            }
            else
            {
                int index = Satıs.Detaylar.FindIndex(x => x.Urun.Id == urun.Id);
                Satıs.Detaylar[index].Adet++;
                Satıs.Detaylar[index].Tutar += urun.Fiyat;
                Satıs.ToplamTutar += urun.Fiyat;
            }
        }

        public void Sil(int urunId)
        {
            int index = Satıs.Detaylar.FindIndex(x => x.Id == urunId);
            if (index != -1)
            {
                Satıs.Detaylar.RemoveAt(index);
            }
        }
    }
}
