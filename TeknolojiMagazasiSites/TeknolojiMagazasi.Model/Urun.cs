using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknolojiMagazasi.Model
{
    [Table("tblUrunler")]
    public class Urun
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int StokAdet { get; set; }
        public decimal Fiyat { get; set; }
        public int MarkaId { get; set; }
        [ForeignKey("MarkaId")]
        public Marka Marka { get; set; }
    }
}
