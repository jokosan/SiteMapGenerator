using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class LoadingSiteMap
    {
        private readonly Parser _parser;
        private readonly LinkValidator _linkValidator;

        public LoadingSiteMap(
            Parser parser,
            LinkValidator linkValidator)
        {
            _parser = parser;
            _linkValidator = linkValidator;
        }

        private const string robots = "robots.txt";
        private const string sitemap = "sitemap.xml";

        public IEnumerable<string> SearchSitemap(string url)
        {
            var resultSitmap = new List<string>();

            var urlRobotsTxt = url + robots;
            var urlSitemap = url + sitemap;

            if (_linkValidator.StatusHost(urlRobotsTxt))
            {
                string fileContent = new WebClient().DownloadString(urlRobotsTxt);

                if (_parser.CheckForSitemapAvailability(fileContent))
                {
                    var delimiters = new string[] { "\n", "\r" };

                    var subs = fileContent
                                 .Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    string linkSiteMap = _parser.ResultUrlSiteMAp(subs);
                    resultSitmap.AddRange(_parser.XMLSiteMap(linkSiteMap));
                }
            }
            else if (_linkValidator.StatusHost(urlSitemap))
            {
                resultSitmap.AddRange(_parser.XMLSiteMap(urlSitemap));
            }
            else
            {
                resultSitmap.Add("Sitemap not found or invalid robots.txt");
            }

            return resultSitmap;
        }
    }
}