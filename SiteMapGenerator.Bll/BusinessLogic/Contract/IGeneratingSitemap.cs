using SiteMapGenerator.Bll.Models.Bll;
using System.Collections.Generic;

namespace SiteMapGenerator.Bll.BusinessLogic.Contract
{
    public interface IGeneratingSitemap
    {
        List<JoinResultBll> Loading(string url, int numberOfLinks);
        bool ValidationAddresses(string url);
    }
}
