using SiteMapGenerator.Bll.Models;
using SiteMapGenerator.Dal.Models.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SiteMapGenerator.Dal.Serveses
{
    public class SaveDbSiteMap
    {
        private readonly IRepository<ArchiveOfRequest> _repositoryArchiveOfRequest;
        private readonly IRepository<PageInfo> _repositoryPageInfo;
        private readonly IRepository<UrlSiteMap> _repositoryUrlSiteMap;

        public SaveDbSiteMap(
            IRepository<ArchiveOfRequest> repositoryArchiveOfRequest,
            IRepository<PageInfo> repositoryPageInfo,
            IRepository<UrlSiteMap> repositoryUrlSiteMap)
        {
            _repositoryArchiveOfRequest = repositoryArchiveOfRequest;
            _repositoryPageInfo = repositoryPageInfo;
            _repositoryUrlSiteMap = repositoryUrlSiteMap;
        }

        public virtual void Save(IEnumerable<UrlResult> urlResults, IEnumerable<UrlSiteMap> urlSiteMaps, int urlId)
        {
            foreach (var itemUrl in urlResults)
            {
                var sitemap = new UrlSiteMap();
                var pageInfo = new PageInfo();

                if (urlSiteMaps.Any(x => x.NameSite.Contains(itemUrl.NameSite)))
                {
                    pageInfo.SitemapId = ExistingRecordId(urlSiteMaps, itemUrl.NameSite);
                }
                else
                {
                    sitemap.ArchiveOfRequestsId = urlId;
                    sitemap.NameSite = itemUrl.NameSite;
                    pageInfo.SitemapId = SaveSitemap(sitemap);
                }

                pageInfo.WebsiteLoadingSpeed = itemUrl.WebsiteLoadingSpeed;
                pageInfo.StatusCode = itemUrl.StatusCode;
                pageInfo.PageTestDate = DateTime.Now;
                pageInfo.Elapsed = itemUrl.Elapsed;
                pageInfo.parseLink = itemUrl.parseLink;
                pageInfo.sitemapLink = itemUrl.sitemapLink;

                _repositoryPageInfo.Add(pageInfo);
                _repositoryPageInfo.SaveChanges();
            }
        }

        public virtual int SaveUserRequest(string url)
        {
            var resultArxiv = _repositoryArchiveOfRequest.GetAll();

            if (resultArxiv.Any(x => x.NameUrl.Contains(url)))
            {
                var resultId = resultArxiv.FirstOrDefault(x => x.NameUrl.Contains(url));
                return resultId.IdArchiveOfRequests;
            }
            else
            {
                var archive = new ArchiveOfRequest();
                archive.NameUrl = url;

                _repositoryArchiveOfRequest.Add(archive);
                _repositoryArchiveOfRequest.SaveChanges();

                return archive.IdArchiveOfRequests;
            }
        }

        private int ExistingRecordId(IEnumerable<UrlSiteMap> urlSiteMaps, string item)
        {
            var result = urlSiteMaps.Where(x => x.NameSite.Contains(item));
            var resultWhere = result.LastOrDefault();

            return resultWhere.IdSitemap;
        }

        private int SaveSitemap(UrlSiteMap row)
        {
            _repositoryUrlSiteMap.Add(row);
            _repositoryUrlSiteMap.SaveChanges();

            return row.IdSitemap;
        }
    }
}
