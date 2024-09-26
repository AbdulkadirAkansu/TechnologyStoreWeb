using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeknolojiMagazasi.Model
{
    public enum Yetkiler
    {
        [Display(Name = "Müdür")]
        Mudur = 1,
        [Display(Name = "Kasiyer")]
        Kasiyer = 2
    }

    [Table("tblKullanıcılar")]
    public class Kullanıcı
    {
        [Key]
        public string EPosta { get; set; }
        public string Parola { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public Yetkiler Yetki { get; set; }
    }
}
