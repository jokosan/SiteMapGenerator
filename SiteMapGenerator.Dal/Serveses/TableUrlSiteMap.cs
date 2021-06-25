using SiteMapGenerator.Dal.Models.Dal;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SiteMapGenerator.Dal.Serveses
{
    public class TableUrlSiteMap
    {
        private readonly IRepository<UrlSiteMap> _repositoryUrlSiteMap;

        public TableUrlSiteMap(
            IRepository<UrlSiteMap> repositoryUrlSiteMap)
        {
            _repositoryUrlSiteMap = repositoryUrlSiteMap;
        }

        public IQueryable<UrlSiteMap> GetSiteMap()
        => _repositoryUrlSiteMap.GetAll();

        public IQueryable<UrlSiteMap> RequestToGetMatchesForGiven(int idArchiveOfRequests)
            => GetSiteMap().Where(x => x.ArchiveOfRequestsId == idArchiveOfRequests);

        public virtual bool SearchLinkBd(string link)
            => GetSiteMap().Any(x => x.NameSite.Contains(link));
               
        public int SaveSitemap(UrlSiteMap row)
        {
            _repositoryUrlSiteMap.Add(row);
            _repositoryUrlSiteMap.SaveChanges();

            return row.IdSitemap;
        }
    }
}
