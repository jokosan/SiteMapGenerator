using SiteMapGenerator.Bll.Models.Bll;
using System.Collections.Generic;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IWebsiteLoadingSpeed
    {
        List<UrlResult> SpeedPageUploads(List<string> url, int? idUrl = null);
    }
}
