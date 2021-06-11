using System.Collections.Generic;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IWebsiteLoadingSpeed
    {
        List<string> SpeedPageUploads(List<string> url, int idUrl);
    }
}
