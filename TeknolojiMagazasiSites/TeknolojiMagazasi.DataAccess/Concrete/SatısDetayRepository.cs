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
    public class SatısDetayRepository : Repository<SatısDetay>, ISatısDetayRepository
    {
        public SatısDetayRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<SatısDetay> GetAllWithUrun()
        {
            return context.Set<SatısDetay>().Include(s => s.Urun).ToList();
        }

        public IEnumerable<SatısDetay> GetAllWithUrun(int satis_id)
        {
            return context.Set<SatısDetay>().Include(s => s.Urun).Where(s => s.SatısId == satis_id).ToList();
        }
    }
}
