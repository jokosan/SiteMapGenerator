using ConsoleSiteMapGenerator.Infrastructure;
using ConsoleSiteMapGenerator.Infrastructure.Constants;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Dal.Serveses;
using System.Linq;

namespace SiteMapGenerator.Dal
{
    public class StartConsole
    {
        private readonly TableArchiveOfRequest _tableArchiveOfRequest;
        private readonly TableUrlSiteMap _tableUrlSiteMap;
        private readonly TablePageInfo _pageInfo;
        private readonly TableUrlResult _tableUrlResult;

        public StartConsole(
           TableArchiveOfRequest tableArchiveOfRequest,
          TableUrlResult tableUrlResult,
          TablePageInfo pageInfo,
          TableUrlSiteMap tableUrlSiteMap)
        {
            _tableArchiveOfRequest = tableArchiveOfRequest;
            _tableUrlResult = tableUrlResult;
            _tableUrlSiteMap = tableUrlSiteMap;
            _pageInfo = pageInfo;
        }

        public void StartMain()
        {
            var linkValidator = new LinkValidator();
            var parser = new HtmlParser(linkValidator);
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
                int idLink = _tableArchiveOfRequest.SaveUserRequest(userUrl);

                var parserPages = loadingPageUrls.ExtractHref(userUrl);
                var parserSitMapXml = loadingSiteMap.SearchSitemap(userUrl);

                userInteraction.Info($"{MessageUsers.numberOfLinks} {MessageUsers.xmlSiteMap} {parserSitMapXml.Count()} {MessageUsers.parserSiteMAp} {parserPages.Count()}");

                var resultLink = webRequestServeses.SpeedPageUploads(loadingPageUrls.ExtractHref(userUrl), loadingSiteMap.SearchSitemap(userUrl));
                _tableUrlResult.Save(resultLink, _tableUrlSiteMap.RequestToGetMatchesForGiven(idLink), idLink);

                printResult.SiteMapPrint(_tableUrlResult.JoinTableUrlSiteMapToPageInfo(idLink)
                        .Where(x => x.PageTestDate.Value.Date == System.DateTime.Now.Date));
            }
            else
            {
                userInteraction.Info(MessageUsers.IncorrectUrl);
            }
        }
    }
}
