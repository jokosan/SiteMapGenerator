using SiteMapGeneratorDal.dbContext;
using SiteMapGeneratorDal.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGeneratorDal.Infrastructure.UnitOfWork.Contract
{
    public interface IUnitOfWork
    {
        void Dispose();

        void Save();

        DbRepository<ArchiveOfRequest> ArchiveOfRequestsUnitOfWork { get; set; }
        DbRepository<UrlSiteMap> SitemapUnitOFWork { get; set; }
        DbRepository<PageInfo> PageInfoUnitOFWork { get; set; }
    }
}
