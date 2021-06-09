using SiteMapGenerator.Bll.Models.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface ILoadingSiteAddresses
    {
        int SaveUserRequest(string url);
        List<string> Loading(string url, int numberOfLinks, int IdUri);
        bool ValidationAddresses(string url);
        IEnumerable<JoinResultBll> GetSitemaps(int id);
        IEnumerable<ArchiveOfRequestBll> Arxiv();
        IEnumerable<JoinResultBll> Arxiv(int id);
        IEnumerable<JoinResultBll> Arxiv(int id, DateTime date);
    }
}
