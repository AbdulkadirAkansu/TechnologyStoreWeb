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
    public class UrunRepository : Repository<Urun>, IUrunRepository
    {
        public UrunRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Urun> GetAllWithMarka()
        {
            return context.Set<Urun>().Include(u => u.Marka).ToList();
        }
    }
}
