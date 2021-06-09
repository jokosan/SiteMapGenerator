using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGeneratorDal.Infrastructure.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class WebsiteLoadingSpeed : IWebsiteLoadingSpeed
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILinkCheck _linkCheck;
        private readonly IUrlSiteMap _urlSiteMap;
        private readonly IPageInfo _pageInfo;

        public WebsiteLoadingSpeed(
            IUnitOfWork unitOfWork,
            ILinkCheck linkCheck,
            IUrlSiteMap urlSiteMap,
            IPageInfo pageInfo)
        {
            _unitOfWork = unitOfWork;
            _linkCheck = linkCheck;
            _urlSiteMap = urlSiteMap;
            _pageInfo = pageInfo;
        }

        public List<string> SpeedPageUploads(List<string> url, int IdUrl)
        {
            var sitemap = new UrlSiteMapBll();
            var pageInfo = new PageInfoBll();
            var listWebExceptionResponse = new List<string>();
            //var sitmapResult = _unitOfWork.SitemapUnitOFWork.Get();
            var sitmapResult = _urlSiteMap.GetTableAll();

            foreach (var item in url)
            {
                try
                {
                    if (_linkCheck.UrlValidation(item))
                    {
                        Stopwatch sw = new Stopwatch();

                        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(item);

                        req.Method = WebRequestMethods.Http.Get;
                        req.AllowAutoRedirect = false;
                        req.Accept = @"*/*";

                        sw.Start(); // start Stopwatch


                        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                        var rescode = (int)res.StatusCode;
                        sw.Stop();
                        res.Close();

                        TimeSpan timeToLoad = sw.Elapsed;

                        if (sitmapResult.Any(x => x.NameSite.Contains(item)))
                        {
                            pageInfo.SitemapId = SaveTableSiteMap(sitmapResult, item);
                        }
                        else
                        {
                            sitemap.ArchiveOfRequestsId = IdUrl;
                            sitemap.NameSite = item;
                            pageInfo.SitemapId = SaveSitemap(sitemap);
                        }

                        pageInfo.WebsiteLoadingSpeed = sw.ElapsedTicks;
                        pageInfo.StatusCode = rescode;
                        pageInfo.PageTestDate = DateTime.Now;
                        pageInfo.LastModified = res.LastModified;
                        pageInfo.Elapsed = sw.Elapsed;

                        _pageInfo.Insert(pageInfo);
                        //_unitOfWork.PageInfoUnitOFWork.Insert(pageInfo);
                        //_unitOfWork.Save();
                    }
                    else
                    {
                        listWebExceptionResponse.Add($"URI Invalid {item}");
                    }

                }
                catch (WebException ex)
                {
                    if (ex.Response == null)
                    {
                        listWebExceptionResponse.Add(ex.Message);
                    }
                    else
                    {
                        if (sitmapResult.Any(x => x.NameSite.Contains(item)))
                        {
                            pageInfo.SitemapId = SaveTableSiteMap(sitmapResult, item);
                        }
                        else
                        {
                            sitemap.ArchiveOfRequestsId = IdUrl;
                            sitemap.NameSite = item;
                            pageInfo.SitemapId = SaveSitemap(sitemap);
                        }

                        pageInfo.StatusCode = (int)((HttpWebResponse)ex.Response).StatusCode;
                        pageInfo.PageTestDate = DateTime.Now;

                        _pageInfo.Insert(pageInfo);
                        //_unitOfWork.PageInfoUnitOFWork.Insert(pageInfo);
                        //_unitOfWork.Save();
                    }
                }
            }

            return listWebExceptionResponse;
        }

        private int SaveTableSiteMap(IEnumerable<UrlSiteMapBll> urlSiteMaps, string item)
        {
            var result = urlSiteMaps.Where(x => x.NameSite.Contains(item));
            var resultWhere = result.LastOrDefault();
            return resultWhere.IdSitemap;
        }

        private int SaveSitemap(UrlSiteMapBll row)
        {
            //_unitOfWork.SitemapUnitOFWork.Insert(row);
            //_unitOfWork.Save();

            _urlSiteMap.Insert(row);
            return row.IdSitemap;
        }
    }
}
