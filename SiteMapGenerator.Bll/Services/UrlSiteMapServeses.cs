using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGenerator.Dal.Models.Dal;
using System.Collections.Generic;
using System.Data;

namespace SiteMapGenerator.Bll.Services
{
    public class UrlSiteMapServeses : IUrlSiteMap
    {
        private readonly IRepository<UrlSiteMap> _repositoryUrlSiteMap;

        public UrlSiteMapServeses(
            IRepository<UrlSiteMap> repositoryUrlSiteMap)
        {
            _repositoryUrlSiteMap = repositoryUrlSiteMap;
        }

        public IEnumerable<UrlSiteMap> GetTableAll()
            => _repositoryUrlSiteMap.GetAll();

        public UrlSiteMap SelectId(int? elementId)
            => _repositoryUrlSiteMap.GetById(elementId.Value);

        public void Insert(UrlSiteMap element)
        {
            _repositoryUrlSiteMap.Add(element);
            _repositoryUrlSiteMap.SaveChanges();
        }

        public void Update(UrlSiteMap elementToUpdate)
        {
            _repositoryUrlSiteMap.Update(elementToUpdate);
            _repositoryUrlSiteMap.SaveChanges();
        }
    }
}
