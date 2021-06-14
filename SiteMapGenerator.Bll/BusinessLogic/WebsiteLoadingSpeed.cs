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
        private readonly ILinkCheck _linkCheck;

        public WebsiteLoadingSpeed(
            ILinkCheck linkCheck
            )
        {
            _linkCheck = linkCheck;
        }

        public List<JoinResultBll> SpeedPageUploads(List<string> url, int? IdUrl = null)
        {
            var resultSiteMapList = new List<JoinResultBll>();

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

                        resultSiteMapList.Add(createJoinResultBll(item, rescode, sw));
                    }
                }
                catch
                {
                    resultSiteMapList.Add(createJoinResultBll(item, 404));
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

        private JoinResultBll createJoinResultBll(string url, int rescode, Stopwatch sw = null)
        {
            var resultJoinResult = new JoinResultBll();

            resultJoinResult.NameSite = url;
            resultJoinResult.StatusCode = rescode;
            resultJoinResult.PageTestDate = DateTime.Now;

            if (sw != null)
            {
                resultJoinResult.WebsiteLoadingSpeed = sw.ElapsedTicks;
                resultJoinResult.Elapsed = sw.Elapsed;
            }

            return resultJoinResult;
        }
    }
}
