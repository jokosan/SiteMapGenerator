using ConsoleSiteMapGenerator.Infrastructure.Constants;
using SiteMapGenerator.Bll.BusinessLogic;
using System.Linq;

namespace ConsoleSiteMapGenerator.Infrastructure
{
    class AppStart
    {
        public void Start()
        {
            var linkValidator = new LinkValidator();
            var parser = new Parser();
            var loadingPageUrls = new LoadingPageUrls(linkValidator, parser);
            var webRequestServeses = new WebRequestServeses(linkValidator);
            var userInteraction = new UserInteraction();
            var printResult = new PrintResult(userInteraction);
            var loadingSiteMap = new LoadingSiteMap(parser, linkValidator);

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
