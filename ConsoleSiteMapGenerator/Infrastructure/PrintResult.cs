using SiteMapGenerator.Bll.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSiteMapGenerator.Infrastructure
{
    public class PrintResult
    {
        private readonly UserInteraction _userInteraction;

        public PrintResult(UserInteraction userInteraction)
        {
            _userInteraction = userInteraction;
        }

        public void SiteMapPrint(IEnumerable<UrlResult> resultUrl)
        {
            var statusCode = resultUrl.Where(x => x.StatusCode == 200); /// ??
            foreach (var item in statusCode)
            {
                _userInteraction.Info($"Url {item.NameSite} | LoadingSpeed  {item.WebsiteLoadingSpeed}");
            }
        }
    }
}
