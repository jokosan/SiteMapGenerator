using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Dal.Models.Dal;
using System;
using System.Collections.Generic;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface ILoadingSiteAddresses : IGeneratingSitemap
    {
        int SaveUserRequest(string url);
        IEnumerable<JoinResultBll> GetSitemaps(int id);
        IEnumerable<ArchiveOfRequest> Arxiv();
        IEnumerable<JoinResultBll> Arxiv(int id);
        IEnumerable<JoinResultBll> Arxiv(int id, DateTime date);
    }
}
