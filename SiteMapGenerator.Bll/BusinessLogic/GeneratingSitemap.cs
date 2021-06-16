using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using System.Collections.Generic;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class GeneratingSitemap : IGeneratingSitemap
    {
        private readonly ILinkValidator _linkCheck;
        private readonly ILoadingPageUrls _loadingPageUrls;
        private readonly IWebsiteLoadingSpeed _websiteLoadingSpeed;

        public GeneratingSitemap(
            ILinkValidator linkCheck,
            ILoadingPageUrls loadingPageUrls,
            IWebsiteLoadingSpeed websiteLoadingSpeed)
        {
            _linkCheck = linkCheck;
            _loadingPageUrls = loadingPageUrls;
            _websiteLoadingSpeed = websiteLoadingSpeed;
        }

        public List<UrlResult> Loading(string url, int numberOfLinks)
            => _websiteLoadingSpeed.SpeedPageUploads(_loadingPageUrls.ExtractHref(url, numberOfLinks));

        public bool ValidationAddresses(string url)
            => _linkCheck.UrlValidation(url);
    }
}
