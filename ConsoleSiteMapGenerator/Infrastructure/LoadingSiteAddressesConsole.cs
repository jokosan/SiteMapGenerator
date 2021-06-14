using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using System.Collections.Generic;

namespace ConsoleSiteMapGenerator.Infrastructure
{
    public class LoadingSiteAddressesConsole : IGeneratingSitemap
    {
        private readonly ILinkCheck _linkCheck;
        private readonly ILoadingPageUrls _loadingPageUrls;
        private readonly IWebsiteLoadingSpeed _websiteLoadingSpeed;

        public LoadingSiteAddressesConsole(
            ILinkCheck linkCheck,
            ILoadingPageUrls loadingPageUrls,
            IWebsiteLoadingSpeed websiteLoadingSpeed)
        {
            _linkCheck = linkCheck;
            _loadingPageUrls = loadingPageUrls;
            _websiteLoadingSpeed = websiteLoadingSpeed;
        }

        public List<JoinResultBll> Loading(string url, int numberOfLinks)
            => _websiteLoadingSpeed.SpeedPageUploads(_loadingPageUrls.ExtractHref(url, numberOfLinks));

        public bool ValidationAddresses(string url)
            => _linkCheck.UrlValidation(url);
    }
}
