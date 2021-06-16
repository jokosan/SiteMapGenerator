using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class WebsiteLoadingSpeed : IWebsiteLoadingSpeed
    {
        private readonly ILinkValidator _linkCheck;

        public WebsiteLoadingSpeed(
            ILinkValidator linkCheck
            )
        {
            _linkCheck = linkCheck;
        }

        public List<UrlResult> SpeedPageUploads(List<string> url)
        {
            var resultSiteMapList = new List<UrlResult>();

            foreach (var item in url)
            {
                try
                {
                    if (_linkCheck.UrlValidation(item))
                    {
                        var sw = new Stopwatch();
                        var req = HttpGet(item);

                        sw.Start();
                        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                        var rescode = (int)res.StatusCode;
                        sw.Stop();

                        res.Close();

                        TimeSpan timeToLoad = sw.Elapsed;

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

        private UrlResult CreateJoinResultBll(string url, int rescode, Stopwatch sw = null)
        {
            var resultJoinResult = new UrlResult();

            resultJoinResult.NameSite = url;
            resultJoinResult.StatusCode = rescode;
            resultJoinResult.PageTestDate = DateTime.Now;

            if (sw != null)
            {
                resultJoinResult.WebsiteLoadingSpeed = sw.ElapsedTicks;
                resultJoinResult.Elapsed = sw.Elapsed.Seconds;
            }

            return resultJoinResult;
        }
    }
}
