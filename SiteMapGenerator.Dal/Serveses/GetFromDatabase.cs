using SiteMapGenerator.Bll.Models;
using SiteMapGenerator.Dal.Models.Dal;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SiteMapGenerator.Dal.Serveses
{
    public class GetFromDatabase
    {
        private readonly IRepository<ArchiveOfRequest> _repositoryArchiveOfRequest;
        private readonly IRepository<PageInfo> _repositoryPageInfo;
        private readonly IRepository<UrlSiteMap> _repositoryUrlSiteMap;

        public GetFromDatabase(
            IRepository<ArchiveOfRequest> repositoryArchiveOfRequest,
            IRepository<PageInfo> repositoryPageInfo,
            IRepository<UrlSiteMap> repositoryUrlSiteMap)
        {
            _repositoryArchiveOfRequest = repositoryArchiveOfRequest;
            _repositoryPageInfo = repositoryPageInfo;
            _repositoryUrlSiteMap = repositoryUrlSiteMap;
        }

        public IQueryable<ArchiveOfRequest> GetArchiveOfRequest()
            => _repositoryArchiveOfRequest.GetAll();

        public IQueryable<UrlSiteMap> GetSiteMap()
            => _repositoryUrlSiteMap.GetAll();

        public IQueryable<PageInfo> GetPageInfos()
            => _repositoryPageInfo.GetAll();

        public IEnumerable<UrlResult> JoinTableUrlSiteMapToPageInfo(int id)
        {
            var siteMapResult = GetSiteMap().Where(x => x.ArchiveOfRequestsId == id);
            var pageInfoResult = GetPageInfos();

            return siteMapResult.Join(pageInfoResult,
              sitemap => sitemap.IdSitemap,
              pageInfo => pageInfo.IdPageInfo,
              (sitemap, pageInfo) => new UrlResult()
              {
                  IdUrlResult = sitemap.IdSitemap,
                  ArchiveOfRequestsId = sitemap.ArchiveOfRequestsId,
                  NameSite = sitemap.NameSite,
                  StatusCode = pageInfo.StatusCode,
                  WebsiteLoadingSpeed = pageInfo.WebsiteLoadingSpeed,
                  PageTestDate = pageInfo.PageTestDate.Value.Date,
                  Elapsed = pageInfo.Elapsed,
                  sitemapLink = pageInfo.sitemapLink,
                  parseLink = pageInfo.parseLink
              });
        }

        public IEnumerable<UrlResult> JoinTableGroup(IEnumerable<UrlResult> joinResultModels)
        {
            return joinResultModels.GroupBy(x => x.IdUrlResult)
                                .Select(y => new UrlResult
                                {
                                    IdUrlResult = y.First().IdUrlResult,
                                    ArchiveOfRequestsId = y.First().ArchiveOfRequestsId,
                                    NameSite = y.First().NameSite,
                                    ElapsedMin = y.Min(x => x.Elapsed),
                                    ElapsedMax = y.Max(x => x.Elapsed),
                                    parseLink = y.First().parseLink,
                                    sitemapLink = y.First().sitemapLink
                                }).OrderBy(o => o.ElapsedMax);
        }
    }
}
