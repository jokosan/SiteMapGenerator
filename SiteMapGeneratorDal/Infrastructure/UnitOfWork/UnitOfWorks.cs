using SiteMapGeneratorDal.dbContext;
using SiteMapGeneratorDal.Infrastructure.Repository;
using SiteMapGeneratorDal.Infrastructure.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGeneratorDal.Infrastructure.UnitOfWork
{
    public class UnitOfWorks : IUnitOfWork, IDisposable
    {
        internal TestTaskForNetDeveloperPositionContext _entities;

        public UnitOfWorks()
        {
            _entities = new TestTaskForNetDeveloperPositionContext();
        }

        private DbRepository<ArchiveOfRequest> ArchiveOfRequestsUW;
        private DbRepository<UrlSiteMap> SitemapUW;
        private DbRepository<PageInfo> PageInfoUW;

        public DbRepository<ArchiveOfRequest> ArchiveOfRequestsUnitOfWork
        {
            get => ArchiveOfRequestsUW ?? (ArchiveOfRequestsUW = new DbRepository<ArchiveOfRequest>(_entities));
            set => ArchiveOfRequestsUW = value;
        }

        public DbRepository<UrlSiteMap> SitemapUnitOFWork
        {
            get => SitemapUW ?? (SitemapUW = new DbRepository<UrlSiteMap>(_entities));
            set => SitemapUW = value;
        }

        public DbRepository<PageInfo> PageInfoUnitOFWork
        {
            get => PageInfoUW ?? (PageInfoUW = new DbRepository<PageInfo>(_entities));
            set => PageInfoUW = value;
        }

        #region Dispose
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Save()
        {
            _entities.SaveChanges();
        }
        #endregion
    }
}
