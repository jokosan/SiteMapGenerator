using AutoMapper;
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
        private readonly IMapper _mapper;

        public WebsiteLoadingSpeed(
            IUnitOfWork unitOfWork,
            ILinkCheck linkCheck,
            IUrlSiteMap urlSiteMap,
            IPageInfo pageInfo,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _linkCheck = linkCheck;
            _urlSiteMap = urlSiteMap;
            _pageInfo = pageInfo;
            _mapper = mapper;
        }

        public List<string> SpeedPageUploads(List<string> url, int IdUrl)
        {
            var sitemap = new UrlSiteMapBll();
            var pageInfo = new PageInfoBll();
            var listWebExceptionResponse = new List<string>();
            var sitmapResult = _mapper.Map<IEnumerable<UrlSiteMapBll>>(_urlSiteMap.GetTableAll());

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

                        sw.Start(); 

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
                        pageInfo.Elapsed = sw.Elapsed;

                        _pageInfo.Insert(pageInfo);
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
            _urlSiteMap.Insert(row);
            return row.IdSitemap;
        }
    }
}
