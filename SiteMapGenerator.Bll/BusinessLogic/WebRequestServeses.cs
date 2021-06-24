using SiteMapGenerator.Bll.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class WebRequestServeses
    {
        private readonly LinkValidator _linkValidator;

        public WebRequestServeses(
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

        private UrlResult CreateJoinResultBll(string url, int rescode, Stopwatch sw, bool parser, bool sitmap) //???
        {
            var resultJoinResult = new UrlResult()
            {
                NameSite = url,
                StatusCode = rescode,
                PageTestDate = DateTime.Now,
                parseLink = parser,
                sitemapLink = sitmap,
                WebsiteLoadingSpeed = sw.ElapsedTicks,
                Elapsed = sw.Elapsed.Milliseconds,
            };

            return resultJoinResult;
        }
    }
}