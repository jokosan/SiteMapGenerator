using System.Collections.Generic;
using System.Net;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class LoadingSiteMap
    {
        private readonly SitemapParser _sitemapParser;
        private readonly LinkValidator _linkValidator;

        public LoadingSiteMap(
            SitemapParser sitemapParser,
            LinkValidator linkValidator)
        {
            _sitemapParser = sitemapParser;
            _linkValidator = linkValidator;
        }

        public virtual IEnumerable<string> SearchSitemap(string userUrl)
        {
            var resultSitmap = new List<string>();

            var urlRobotsTxt = userUrl + "robots.txt";
            var urlSitemap = userUrl + "sitemap.xml";

            if (_linkValidator.StatusHost(urlSitemap))
            {
                resultSitmap.AddRange(_sitemapParser.XMLSiteMap(urlSitemap));
            }
            else if (_linkValidator.StatusHost(urlRobotsTxt))
            {
                string fileContent = new WebClient().DownloadString(urlRobotsTxt);

                if (_sitemapParser.CheckForSitemapAvailability(fileContent))
                {
                    var resultSitemapCrawler = _sitemapParser.XMLSiteMap(_sitemapParser.ReturnUrlSitemap(fileContent));

                    resultSitmap.AddRange(resultSitemapCrawler);
                }
            }
            else
            {
                resultSitmap.Add("Sitemap not found or invalid robots.txt");
            }

            return resultSitmap;
        }
    }
}