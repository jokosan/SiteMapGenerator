using System.Collections.Generic;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface ILoadingPageUrls
    {
        List<string> ExtractHref(string URL, int countLink);
    }
}
