using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknolojiMagazasi.Model
{
    [Table("tblSatısDetaylar")]
    public class SatısDetay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Adet { get; set; }
        public decimal Tutar { get; set; }

        [ForeignKey("Urun")]
        public int UrunId { get; set; }
        public Urun Urun { get; set; }


        public int SatısId { get; set; }
        //[ForeignKey("SatısId")]
        public Satıs Satıs { get; set; }
    }
}
