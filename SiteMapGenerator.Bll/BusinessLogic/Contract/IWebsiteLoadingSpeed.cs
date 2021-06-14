using SiteMapGenerator.Bll.Models.Bll;
using System.Collections.Generic;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IWebsiteLoadingSpeed
    {
        List<JoinResultBll> SpeedPageUploads(List<string> url, int? idUrl = null);
    }
}
