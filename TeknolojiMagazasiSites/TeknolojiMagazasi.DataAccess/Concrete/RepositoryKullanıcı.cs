using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasi.DataAccess.Abstract;
using TeknolojiMagazasi.Model;

namespace TeknolojiMagazasi.DataAccess.Concrete
{
    public class RepositoryKullanıcı : Repository<Kullanıcı>,IKullanıcıRepository
    {
        public RepositoryKullanıcı(DbContext context) : base(context)
        {
        }

        public bool Login(string eposta, string parola)
        {
            if (context.Set<Kullanıcı>().FirstOrDefault(x =>
            x.EPosta.ToLower().Equals(eposta.ToLower()) &&
            x.Parola.Equals(parola)) != null)

                return true;
            else
                return false;
        }
    }
}
