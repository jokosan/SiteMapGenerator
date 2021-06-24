using SiteMapGenerator.Bll.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class WebsiteLoadingSpeed
    {
        private readonly LinkValidator _linkValidator;

        public WebsiteLoadingSpeed(
            LinkValidator linkValidator)
        {
            _linkValidator = linkValidator;
        }

        public virtual List<UrlResult> SpeedPageUploads(IEnumerable<string> linkParser, IEnumerable<string> linkSitemap)
        {
            var resultLink = linkParser.Union(linkSitemap).Distinct();

            var resultSiteMapList = new List<UrlResult>();

            foreach (var item in resultLink)
            {
                try
                {
                    if (_linkValidator.CheckURLValid(item))
                    {
                        var sw = new Stopwatch();
                        var req = HttpGet(item);

                        sw.Start();
                        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                        var rescode = (int)res.StatusCode;
                        sw.Stop();

                        res.Close();

                        resultSiteMapList.Add(CreateJoinResultBll(item, rescode, sw, linkParser.Any(x => x.Contains(item)), linkSitemap.Any(x => x.Contains(item))));
                    }
                }
                catch
                {
                }
            }

            return resultSiteMapList;
        }

        private HttpWebRequest HttpGet(string url)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);

            req.Method = WebRequestMethods.Http.Get;
            req.AllowAutoRedirect = false;
            req.Accept = @"*/*";

            return req;
        }

        private UrlResult CreateJoinResultBll(string url, int rescode, Stopwatch sw, bool parser, bool sitmap)
        {
            var resultJoinResult = new UrlResult();

            resultJoinResult.NameSite = url;
            resultJoinResult.StatusCode = rescode;
            resultJoinResult.PageTestDate = DateTime.Now;
            resultJoinResult.parseLink = parser;
            resultJoinResult.sitemapLink = sitmap;

            if (sw != null)
            {
                resultJoinResult.WebsiteLoadingSpeed = sw.ElapsedTicks;
                resultJoinResult.Elapsed = sw.Elapsed.Milliseconds;
            }

            return resultJoinResult;
        }
    }
}
