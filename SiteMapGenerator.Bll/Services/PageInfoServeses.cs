using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGenerator.Dal.Models.Dal;
using System.Collections.Generic;
using System.Data;

namespace SiteMapGenerator.Bll.Services
{
    public class PageInfoServeses : IPageInfo
    {
        private readonly IRepository<PageInfo> _repositoryPageInfo;

        public PageInfoServeses(
            IRepository<PageInfo> repositoryPageInfo)
        {
            _repositoryPageInfo = repositoryPageInfo;
        }

        public IEnumerable<PageInfo> GetTableAll()
            => _repositoryPageInfo.GetAll();

        public PageInfo SelectId(int? elementId)
           => _repositoryPageInfo.GetById(elementId.Value);

        public void Insert(PageInfo element)
        {
            _repositoryPageInfo.Add(element);
            _repositoryPageInfo.SaveChanges();
        }

        public void Update(PageInfo elementToUpdate)
        {
            _repositoryPageInfo.Update(elementToUpdate);
            _repositoryPageInfo.SaveChanges();
        }
    }
}
