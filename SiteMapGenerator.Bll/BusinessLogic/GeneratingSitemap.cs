using SiteMapGenerator.Bll.Models;
using System.Collections.Generic;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class GeneratingSitemap
    {
        private readonly LinkValidator _linkCheck;
        private readonly LoadingPageUrls _loadingPageUrls;
        private readonly WebsiteLoadingSpeed _websiteLoadingSpeed;

        public GeneratingSitemap(
            LinkValidator linkCheck,
            LoadingPageUrls loadingPageUrls,
            WebsiteLoadingSpeed websiteLoadingSpeed)
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
