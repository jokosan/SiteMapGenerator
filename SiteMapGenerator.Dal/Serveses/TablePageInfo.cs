using SiteMapGenerator.Dal.Models.Dal;
using System.Data;
using System.Linq;

namespace SiteMapGenerator.Dal.Serveses
{
    public class TablePageInfo
    {
        private readonly IRepository<PageInfo> _repositoryPageInfo;

        public TablePageInfo(
             IRepository<PageInfo> repositoryPageInfo)
        {
            _repositoryPageInfo = repositoryPageInfo;
        }

        public IQueryable<PageInfo> GetPageInfos()
          => _repositoryPageInfo.GetAll();

        public virtual void SavePageInfo(PageInfo row)
        {
            _repositoryPageInfo.Add(row);
            _repositoryPageInfo.SaveChanges();
        }
    }
}
