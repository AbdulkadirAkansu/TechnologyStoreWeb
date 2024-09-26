﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasi.Model;

namespace TeknolojiMagazasi.DataAccess.Abstract
{
    public interface IUrunRepository : IRepository<Urun>
    {
        IEnumerable<Urun> GetAllWithMarka();
    }
}
