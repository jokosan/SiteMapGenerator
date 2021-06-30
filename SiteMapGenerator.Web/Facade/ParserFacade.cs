using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Bll.Models;
using System.Collections.Generic;

namespace SiteMapGenerator.Web.Facade
{
    public class ParserFacade
    {
        private readonly LinkValidator _linkValidator;
        private readonly LoadingPageUrls _loadingPageUrls;
        private readonly LoadingSiteMap _loadingSiteMap;
        private readonly WebRequestServeses _webRequestServeses;

        public ParserFacade(
          LinkValidator linkValidator,
          LoadingPageUrls loadingPageUrls,
          LoadingSiteMap loadingSiteMap,
          WebRequestServeses webRequestServeses)
        {
            _linkValidator = linkValidator;
            _loadingPageUrls = loadingPageUrls;
            _loadingSiteMap = loadingSiteMap;
            _webRequestServeses = webRequestServeses;
        }

        public IEnumerable<UrlResult> FindLinks(string url)
        {
            var htmlParser = _loadingPageUrls.ExtractHref(url);
            var searchSitemap = _loadingSiteMap.SearchSitemap(url);

            return _webRequestServeses.SpeedPageUploads(htmlParser, searchSitemap);
        }

        public bool CheckLink(string urlUser)
            => _linkValidator.CheckURLValid(urlUser);
    }
}
