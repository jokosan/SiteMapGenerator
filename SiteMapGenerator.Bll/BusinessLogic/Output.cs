using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class Output : IOutput
    {
        private readonly IUrlSiteMap _urlSiteMap;
        private readonly IPageInfo _pageInfo;

        public Output(
            IUrlSiteMap urlSiteMap,
            IPageInfo pageInfo)
        {
            _urlSiteMap = urlSiteMap;
            _pageInfo = pageInfo;
        }

        public IEnumerable<JoinResultBll> JoinTableGroup(IEnumerable<JoinResultBll> joinResultModels)
        {
            return joinResultModels.GroupBy(x => x.IdSitemap)
                                .Select(y => new JoinResultBll
                                {
                                    IdSitemap = y.First().IdSitemap,
                                    ArchiveOfRequestsId = y.First().ArchiveOfRequestsId,
                                    NameSite = y.First().NameSite,
                                    ElapsedMin = y.Min(x => x.Elapsed),
                                    ElapsedMax = y.Max(x => x.Elapsed)
                                }).OrderBy(o => o.ElapsedMax);
        }

        public IEnumerable<JoinResultBll> JoinTable(int id)
        {
            var siteMapResult = _urlSiteMap.GetTableAll();
            var siteMapResultWhere =  siteMapResult.Where(x => x.ArchiveOfRequestsId == id);

            var pageInfoResult = _pageInfo.GetTableAll();

            var resultJoin = (from t1 in siteMapResult
                              join t2 in pageInfoResult on t1.IdSitemap equals t2.SitemapId
                              select new JoinResultBll()
                              {
                                  IdSitemap = t1.IdSitemap,
                                  ArchiveOfRequestsId = t1.ArchiveOfRequestsId,
                                  NameSite = t1.NameSite,
                                  StatusCode = t2.StatusCode,
                                  WebsiteLoadingSpeed = t2.WebsiteLoadingSpeed,
                                  PageTestDate = t2.PageTestDate.Value.Date,
                                  Elapsed = t2.Elapsed
                              });

            return resultJoin.AsEnumerable();
        }
    }
}
