using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasi.DataAccess.Concrete;
using TeknolojiMagazasi.Model;

namespace TeknolojiMagazasi.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private readonly TMDatabaseContext context;

        private UrunRepository urunRepo;
        private Repository<Marka> markaRepo;
        private Repository<Satıs> satısRepo;
        private SatısDetayRepository satısDetayRepo;
        private RepositoryKullanıcı kullanıcıRepo;

        public UrunRepository UrunWork
        {
            get
            {
                if (urunRepo == null)
                    urunRepo = new UrunRepository(context);
                return urunRepo;
            }
        }

        public Repository<Marka> MarkaWork
        {
            get
            {
                if (markaRepo == null)
                    markaRepo = new Repository<Marka>(context);
                return markaRepo;
            }
        }

        public Repository<Satıs> SatısWork
        {
            get
            {
                if (satısRepo == null)
                    satısRepo = new Repository<Satıs>(context);
                return satısRepo;
            }
        }

        public SatısDetayRepository SatısDetayWork
        {
            get
            {
                if (satısDetayRepo == null)
                    satısDetayRepo = new SatısDetayRepository(context);
                return satısDetayRepo;
            }
        }

        public RepositoryKullanıcı KullanıcıWork
        {
            get
            {
                if (kullanıcıRepo == null)
                    kullanıcıRepo = new RepositoryKullanıcı(context);
                return kullanıcıRepo;
            }
        }

        public UnitOfWork()
        {
            context = new TMDatabaseContext();
        }

        public void Save()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Dispose()
        {
            urunRepo?.Dispose();
            markaRepo?.Dispose();
            kullanıcıRepo?.Dispose();
            satısRepo?.Dispose();
            satısDetayRepo?.Dispose();
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
