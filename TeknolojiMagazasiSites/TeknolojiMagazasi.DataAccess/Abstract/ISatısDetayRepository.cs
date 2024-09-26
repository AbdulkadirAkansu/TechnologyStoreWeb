using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasi.Model;

namespace TeknolojiMagazasi.DataAccess.Abstract
{
    public interface ISatısDetayRepository : IRepository<SatısDetay>
    {
        IEnumerable<SatısDetay> GetAllWithUrun();
        IEnumerable<SatısDetay> GetAllWithUrun(int satis_id);
    }
}
