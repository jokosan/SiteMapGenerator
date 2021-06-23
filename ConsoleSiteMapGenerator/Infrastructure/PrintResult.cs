using SiteMapGenerator.Bll.Models;
using System.Collections.Generic;

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
            foreach (var item in resultUrl)
            {
                _userInteraction.Info($"Url {item.NameSite} | LoadingSpeed  {item.Elapsed}");
            }
        }
    }
}
