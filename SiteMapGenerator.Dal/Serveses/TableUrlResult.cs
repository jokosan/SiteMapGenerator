using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Bll.Models;
using SiteMapGenerator.Dal.Models.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Dal.Serveses
{
    public class TableUrlResult
    {
        private readonly TablePageInfo _tablePageInfo;
        private readonly TableUrlSiteMap _tableUrlSiteMap;
        private readonly LoadingPageUrls _loadingPageUrls;
        private readonly LoadingSiteMap _loadingSiteMap;
        private readonly WebRequestServeses _webRequestServeses;
        private readonly TableArchiveOfRequest _tableArchiveOfRequest;

        public TableUrlResult(
            TablePageInfo tablePageInfo,
            TableUrlSiteMap tableUrlSiteMap,
            LoadingPageUrls loadingPageUrls,
            LoadingSiteMap loadingSiteMap,
            WebRequestServeses webRequestServeses,
            TableArchiveOfRequest tableArchiveOfRequest)
        {
            _tablePageInfo = tablePageInfo;
            _tableUrlSiteMap = tableUrlSiteMap;
            _loadingPageUrls = loadingPageUrls;
            _loadingSiteMap = loadingSiteMap;
            _webRequestServeses = webRequestServeses;
            _tableArchiveOfRequest = tableArchiveOfRequest;
        }

        public virtual IEnumerable<UrlResult> JoinTableUrlSiteMapToPageInfo(int id)
        {
            var siteMapResult = _tableUrlSiteMap.GetSiteMap().Where(x => x.ArchiveOfRequestsId == id);
            var pageInfoResult = _tablePageInfo.GetPageInfo();

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
                  SitemapLink = pageInfo.sitemapLink,
                  ParseLink = pageInfo.parseLink
              });
        }

        public virtual IEnumerable<UrlResult> JoinTableGroup(IEnumerable<UrlResult> joinResultModels)
        {
            return joinResultModels.GroupBy(x => x.IdUrlResult)
                                .Select(y => new UrlResult
                                {
                                    IdUrlResult = y.First().IdUrlResult,
                                    ArchiveOfRequestsId = y.First().ArchiveOfRequestsId,
                                    NameSite = y.First().NameSite,
                                    ElapsedMin = y.Min(x => x.Elapsed),
                                    ElapsedMax = y.Max(x => x.Elapsed),
                                    ParseLink = y.First().ParseLink,
                                    SitemapLink = y.First().SitemapLink
                                }).OrderBy(o => o.ElapsedMax);
        }

        public virtual void Save(IEnumerable<UrlResult> urlResults, IEnumerable<UrlSiteMap> urlSiteMaps, int urlId)
        {
            foreach (var itemUrl in urlResults)
            {
                var sitemap = new UrlSiteMap();
                var pageInfo = new PageInfo();

                if (_tableUrlSiteMap.SearchLinkBd(itemUrl.NameSite))
                {
                    pageInfo.SitemapId = ExistingRecordId(urlSiteMaps, itemUrl.NameSite);
                }
                else
                {
                    sitemap.ArchiveOfRequestsId = urlId;
                    sitemap.NameSite = itemUrl.NameSite;
                    pageInfo.SitemapId = _tableUrlSiteMap.SaveSitemap(sitemap);
                }

                pageInfo.WebsiteLoadingSpeed = itemUrl.WebsiteLoadingSpeed;
                pageInfo.StatusCode = itemUrl.StatusCode;
                pageInfo.PageTestDate = DateTime.Now;
                pageInfo.Elapsed = itemUrl.Elapsed;
                pageInfo.parseLink = itemUrl.ParseLink;
                pageInfo.sitemapLink = itemUrl.SitemapLink;

                _tablePageInfo.SavePageInfo(pageInfo);
            }
        }

        public IEnumerable<UrlResult> SerQueryResult(int id, DateTime date)
            => JoinTableUrlSiteMapToPageInfo(id).Where(x => x.PageTestDate.Value.Date == date.Date);

        private int ExistingRecordId(IEnumerable<UrlSiteMap> urlSiteMaps, string item)
        {
            var result = urlSiteMaps.Where(x => x.NameSite.Contains(item));
            var resultWhere = result.LastOrDefault();

            return resultWhere.IdSitemap;
        }

        public IEnumerable<UrlResult> ResultGroupJoin(int id)
        {
            var resultJoin = JoinTableUrlSiteMapToPageInfo(id);

            return JoinTableGroup(resultJoin);
        }

        public IEnumerable<UrlResult> ResultArxivDetails(int? id, DateTime? date)
        {
            if (date == null)
            {
                return JoinTableUrlSiteMapToPageInfo(id.Value);
            }
            else
            {
                return SerQueryResult(id.Value, date.Value);
            }
        }

        public int SearchLinksAndSaveResult(string url)
        {
            var idLink = _tableArchiveOfRequest.SaveUserRequest(url);

            var htmlParser = _loadingPageUrls.ExtractHref(url);
            var searchSitemap = _loadingSiteMap.SearchSitemap(url);

            var resultSpeedPageUploads = _webRequestServeses.SpeedPageUploads(htmlParser, searchSitemap);
            var getByIdArchiveOfRequests = _tableUrlSiteMap.RequestToGetMatchesForGiven(idLink);

            Save(resultSpeedPageUploads, getByIdArchiveOfRequests, idLink);

            return idLink;
        }
    }
}
