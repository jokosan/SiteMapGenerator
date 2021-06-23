using SiteMapGenerator.Bll.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public virtual List<UrlResult> SpeedPageUploads(List<string> url)
        {
            var resultSiteMapList = new List<UrlResult>();

            foreach (var item in url)
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
                        var sc = res.StatusDescription;
                        sw.Stop();

                        res.Close();

                        resultSiteMapList.Add(CreateJoinResultBll(item, rescode, sw));
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

        private UrlResult CreateJoinResultBll(string url, int rescode, Stopwatch sw)
        {
            var resultJoinResult = new UrlResult();

            resultJoinResult.NameSite = url;
            resultJoinResult.StatusCode = rescode;
            resultJoinResult.PageTestDate = DateTime.Now;

            if (sw != null)
            {
                resultJoinResult.WebsiteLoadingSpeed = sw.ElapsedTicks;
                resultJoinResult.Elapsed = sw.Elapsed.Milliseconds;
            }

            return resultJoinResult;
        }
    }
}