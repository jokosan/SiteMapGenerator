using ConsoleSiteMapGenerator.Infrastructure;
using ConsoleSiteMapGenerator.Infrastructure.Constants;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Dal.Serveses;
using System.Linq;

namespace SiteMapGenerator.Dal
{
    public class StartConsole
    {
        private readonly SaveDbSiteMap _saveDbSiteMap;
        private readonly GetFromDatabase _getFromDatabase;

        public StartConsole(
            SaveDbSiteMap saveDbSiteMap,
            GetFromDatabase getFromDatabase)
        {
            _saveDbSiteMap = saveDbSiteMap;
            _getFromDatabase = getFromDatabase;
        }

        public void StartMain()
        {
            var linkValidator = new LinkValidator();
            var parser = new HtmlParser();
            var loadingPageUrls = new LoadingPageUrls(parser);
            var webRequestServeses = new WebRequestServeses(linkValidator);
            var userInteraction = new UserInteraction();
            var printResult = new PrintResult(userInteraction);
            var sitemapParser = new SitemapParser();
            var loadingSiteMap = new LoadingSiteMap(sitemapParser, linkValidator);

            userInteraction.Info(MessageUsers.Start);
            string userUrl = userInteraction.UserValueInput();

            if (linkValidator.CheckURLValid(userUrl))
            {
                userInteraction.Info(MessageUsers.Waiting);

                var parserPages = loadingPageUrls.ExtractHref(userUrl);
                var parserSitMapXml = loadingSiteMap.SearchSitemap(userUrl);

                userInteraction.Info($"{MessageUsers.numberOfLinks} {MessageUsers.xmlSiteMap} {parserSitMapXml.Count()} {MessageUsers.parserSiteMAp} {parserPages.Count()}");

                printResult.SiteMapPrint(webRequestServeses.SpeedPageUploads(parserPages, parserSitMapXml));
            }
            else
            {
                userInteraction.Info(MessageUsers.IncorrectUrl);
            }
        }
    }
}
